using System.Configuration;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Diagnostics;
using System.IO;

namespace Headspring.Employee.DAL {

	public static class ConfigUtils {
		#region "Public Static Properties"
		// ----------------------------------------------------------------------------------------------------

		/// <summary>
		/// Property NetworkUserName
		/// </summary>
		public static string NetworkUserName {
			get {
				return WindowsIdentity.GetCurrent().Name;
			}
		}


		#endregion
		#region "Public Static Methods"
		// ----------------------------------------------------------------------------------------------------


		/// <summary>
		/// Static Method AppSettingValue
		/// </summary>
		/// <param name="AppSettingKeyName"></param>
		/// <returns>
		/// AppSettingValue string; Empty string if AppSettingKey is not listed in Config file
		/// </returns>
		public static string AppSettingValue(string AppSettingKey) {
			string retVal = string.Empty;

			if (ConfigurationManager.AppSettings[AppSettingKey] != null)
				retVal = ConfigurationManager.AppSettings[AppSettingKey].ToString();

			return retVal;
		}

		/// <summary>
		/// Static Method ConnectionStringValue
		/// </summary>
		/// <param name="Encrypt">Specifies whether to encrypt the entire Connection String Section</param>
		/// <returns>
		/// Connection String; Empty string if ConnName Setting 
		/// or ConnectionString[AppSetting_ConnStringName] is missing
		/// </returns>
		/// <remarks>
		/// Looks for an AppSetting value for [AppSetting_ConnStringName] in the Config file which tells which
		/// Connection string name to get.  Then tries to retrieve the Connection String.
		/// 
		/// If Encrypt is set to true, it will Encrypt the entire Connection String section.
		/// </remarks>
		public static string ConnectionStringValue(string AppSetting_ConnStringName, bool Encrypt = false) {

			string ConnStr = string.Empty;
			string ConnName = string.Empty;

			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			ConnectionStringsSection cSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

			if (AppSettingValue(AppSetting_ConnStringName).Length > 0) {
				ConnName = AppSettingValue(AppSetting_ConnStringName).ToString();

				if (cSection.ConnectionStrings[ConnName] != null)
					ConnStr = cSection.ConnectionStrings[ConnName].ConnectionString;
			}

			// Encrypt the Connection String section if not already encrypted
			if (Encrypt) {
				if (!cSection.SectionInformation.IsProtected) {
					cSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
					config.Save();
				}
			}

			return ConnStr;
		}

		/// <summary>
		/// Method GetConnection
		/// </summary>
		/// <param name="ConnectionSettingName"></param>
		/// <returns>SqlConnection</returns>
        /// <remarks>
        /// Use DBFilePath AppSetting to get the relative address of a DB File like .mdf or .mdb .
        /// To set up, specify attachdbfilename=|DBFilePath|\HSEmployeeDB.mdf in the ConnectionString keyed by ConnectionSettingName.
        /// Then place <add key="DBFilePath" value="..\..\..\{ProjectName}\App_Data\"> in the appsettings section
        /// Where the dbfile is in the App_Data directory.
        /// </remarks>
		public static SqlConnection GetConnection(string ConnectionSettingName) {
            
            string connString = ConfigUtils.ConnectionStringValue(ConnectionSettingName);
            string dataDir = AppSettingValue("DBFilePath");
            
            if (dataDir.Length > 0) {
                string absDataDir = Path.GetFullPath(dataDir);
                System.AppDomain.CurrentDomain.SetData("DataDirectory", absDataDir);
            }
            
            return new SqlConnection(connString);
		}


		#endregion
	}
}
