using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;
using Headspring.Employee.Models;


namespace Headspring.Employee.DAL {

	internal class DefaultEmployeeStore : IEmployeeStore {

		#region "Private Constants"

		private const string CONN_NAME = "DBConnName";

		private const string Employee_TableName = "Employees";
		private const string Employee_ViewName = "vw_Employees";

		private const string User_TableName = "Users";
		private const string User_ViewName = "vw_Users";

		private const string Location_TableName = "Locations";
		private const string Job_TableName = "Jobs";
		private const string PhoneType_TableName = "PhoneTypes";
		private const string Role_TableName = "Roles";
		private const string Phone_TableName = "Phones";

		// ----------------------------------------------------------------------------------------------------
		private const string GetEmployeeListSQL = @"SELECT DISTINCT
													EmployeeID
													, FName
													, LName
													, MI
													, eMail
													, JobID
													, JobCode
													, JobDescr
													, LocationID
													, LocationDescr
													, City
													, State
                                                  FROM  " + Employee_ViewName;

		private const string GetEmployeeSQL = @"SELECT
												EmployeeID
												, FName
												, LName
												, MI
												, eMail
												, JobID
												, JobCode
												, JobDescr
												, LocationID
												, LocationDescr
												, City
												, State
                                                FROM  " + Employee_ViewName +
											@" WHERE  EmployeeID = @EmpID";

		private const string GetEmpPhoneListSQL = @"SELECT
														EmployeeID
														, IsPrimary
														, PhoneNumber
														, Extension
														, PhoneTypeID
														FROM " + Phone_TableName +
															  @" WHERE EmployeeID = @EmpID";

		// ----------------------------------------------------------------------------------------------------
		private const string GetLocationListSQL = @"SELECT
													LocationID
													, LocationDescr
													, City
													, State AS ST
													FROM " + Location_TableName;

		private const string GetLocationSQL = @"SELECT
												LocationID
												, LocationDescr
												, City
												, State AS ST
												FROM " + Location_TableName +
											@" WHERE  LocationID = @LocID";


		// ----------------------------------------------------------------------------------------------------
		private const string GetJobListSQL = @"SELECT
													JobID
													, JobCode
													, JobDescr
													FROM " + Job_TableName;

		private const string GetJobSQL = @"SELECT
											JobID
											, JobCode
											, JobDescr
											FROM " + Job_TableName +
										@" WHERE  JobID = @JobID";

		// ----------------------------------------------------------------------------------------------------
		private const string GetPhoneTypeListSQL = @"SELECT
													PhoneTypeID
													, TypeDescription AS TypeDescr
													FROM " + PhoneType_TableName;

		private const string GetPhoneTypeSQL = @"SELECT
												PhoneTypeID
												, TypeDescription AS TypeDescr
											FROM " + PhoneType_TableName +
										@" WHERE  PhoneTypeID = @TypeID";

		// ----------------------------------------------------------------------------------------------------
		private const string GetUserListSQL = @"SELECT
                                                UserID
                                                , UserName
												, EmpID
                                                , RoleID
                                                , RoleName
                                                FROM " + User_ViewName;

		#endregion
		#region "Public Properties"

		public int MessageID { get; set; }
		public string MessageString { get; set; }

		public int RecordCount { get; private set; }


		#endregion
		#region "Public Methods"

		/// <summary>
		///		Method GetEmployeeList
		/// </summary>
		/// <returns></returns>
		public IEnumerable<EmployeeInfo> GetEmployeeList() {

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {
					var cmd = new SqlCommand(GetEmployeeListSQL, conn);
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i;
						while (reader.Read()) {
							i = -1;
							int employeeID = reader.GetInt32(++i);
							string fName = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string lName = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string mi = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string email = reader.IsDBNull(++i) ? null : reader.GetString(i);
							int? jobID = reader.IsDBNull(++i) ? (int?)null : reader.GetInt32(i);
							string jobCode = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string jobDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);
							int? locationID = reader.IsDBNull(++i) ? (int?)null : reader.GetInt32(i);
							string locationDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string city = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string st = reader.IsDBNull(++i) ? null : reader.GetString(i);

							yield return new EmployeeInfo() {
								EmpID = employeeID,
								FirstName = fName,
								LastName = lName,
								MiddleName = mi,
								Email = email,
								JobID = jobID,
								LocationID = locationID,
								JobCode = jobCode,
								JobDescr = jobDescr,
								LocationDescr = locationDescr,
								City = city,
								ST = st
							};
						}
					}
				}
			}
		}

		/// <summary>
		///		Method GetEmployee
		/// </summary>
		/// <param name="EmpID"></param>
		/// <returns></returns>
		public EmployeeInfo GetEmployee(int EmpID) {

			EmployeeInfo retVal = null;

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {
					var cmd = new SqlCommand(GetEmployeeSQL, conn);
					cmd.Parameters.Add(new SqlParameter { ParameterName = "EmpID", Value = EmpID });
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i = -1;
						if (reader.Read()) {

							int employeeID = reader.GetInt32(++i);
							string fName = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string lName = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string mi = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string email = reader.IsDBNull(++i) ? null : reader.GetString(i);
							int? jobID = reader.IsDBNull(++i) ? (int?)null : reader.GetInt32(i);
							string jobCode = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string jobDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);
							int? locationID = reader.IsDBNull(++i) ? (int?)null : reader.GetInt32(i);
							string locationDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string city = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string st = reader.IsDBNull(++i) ? null : reader.GetString(i);

							retVal = new EmployeeInfo() {
								EmpID = employeeID,
								FirstName = fName,
								LastName = lName,
								MiddleName = mi,
								Email = email,
								JobID = jobID,
								LocationID = locationID,
								JobCode = jobCode,
								JobDescr = jobDescr,
								LocationDescr = locationDescr,
								City = city,
								ST = st
							};
						}
					}
				}
			}
			return retVal;
		}

		/// <summary>
		///		Method GetLocationList
		/// </summary>
		/// <returns></returns>
		public IEnumerable<LocationInfo> GetLocationList() {

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {

					var cmd = new SqlCommand(GetLocationListSQL, conn);
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i;
						while (reader.Read()) {
							i = -1;
							int locationID = reader.GetInt32(++i);
							string locationDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string city = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string st = reader.IsDBNull(++i) ? null : reader.GetString(i);

							yield return new LocationInfo() {
								LocationID = locationID,
								LocationDescr = locationDescr,
								City = city,
								ST = st
							};
						}
					}
				}
			}
		}

		/// <summary>
		/// Method GetLocation
		/// </summary>
		/// <param name="LocID"></param>
		/// <returns></returns>
		public LocationInfo GetLocation(int LocID) {

			LocationInfo retVal = null;

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {

					var cmd = new SqlCommand(GetLocationSQL, conn);
					cmd.Parameters.Add(new SqlParameter { ParameterName = "LocID", Value = LocID });
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i = -1;
						if (reader.Read()) {
							int locationID = reader.GetInt32(++i);
							string locationDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string city = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string st = reader.IsDBNull(++i) ? null : reader.GetString(i);

							retVal = new LocationInfo() {
								LocationID = locationID,
								LocationDescr = locationDescr,
								City = city,
								ST = st
							};
						}
					}
				}
			}
			return retVal;
		}

		/// <summary>
		///     Method GetJobList
		/// </summary>
		/// <returns></returns>
		public IEnumerable<JobInfo> GetJobList() {

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {

					var cmd = new SqlCommand(GetJobListSQL, conn);
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i;
						while (reader.Read()) {
							i = -1;
							int jobID = reader.GetInt32(++i);
							string jobCode = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string jobDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);

							yield return new JobInfo() {
								JobID = jobID,
								JobCode = jobCode,
								JobDescr = jobDescr
							};
						}
					}
				}
			}
		}

		/// <summary>
		///     Method GetJob
		/// </summary>
		/// <param name="JobID"></param>
		/// <returns></returns>
		public JobInfo GetJob(int JobID) {

			JobInfo retVal = null;

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {

					var cmd = new SqlCommand(GetJobSQL, conn);
					cmd.Parameters.Add(new SqlParameter { ParameterName = "JobID", Value = JobID });
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i;
						if (reader.Read()) {
							i = -1;
							int jobID = reader.GetInt32(++i);
							string jobCode = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string jobDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);

							retVal = new JobInfo() {
								JobID = jobID,
								JobCode = jobCode,
								JobDescr = jobDescr
							};
						}
					}
				}
			}
			return retVal;
		}

		/// <summary>
		///     Method GetEmployeePhones
		/// </summary>
		/// <param name="EmpID"></param>
		/// <returns></returns>
		public IEnumerable<Models.PhoneInfo> GetEmployeePhones(int EmpID) {

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {

					var cmd = new SqlCommand(GetEmpPhoneListSQL, conn);
					cmd.Parameters.Add(new SqlParameter { ParameterName = "EmpID", Value = EmpID });
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i;
						while (reader.Read()) {
							i = -1;
							int employeeID = reader.GetInt32(++i);
							bool isPrimary = reader.GetBoolean(++i);
							string phoneNumber = reader.IsDBNull(++i) ? null : reader.GetString(i);
							string ext = reader.IsDBNull(++i) ? null : reader.GetString(i);
							int? typeID = reader.IsDBNull(++i) ? (int?)null : reader.GetInt32(i);
							//string typeDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);

							yield return new PhoneInfo() {
								EmployeeID = employeeID,
								IsPrimary = isPrimary,
								PhoneNumber = phoneNumber,
								Extension = ext,
								TypeID = typeID
								//TypeDescr = typeDescr
							};
						}
					}
				}
			}
		}

		/// <summary>
		///     Method GetPhoneTypeList
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Models.PhoneTypeInfo> GetPhoneTypeList() {

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {

					var cmd = new SqlCommand(GetPhoneTypeListSQL, conn);
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i;
						while (reader.Read()) {
							i = -1;
							int phoneTypeID = reader.GetInt32(++i);
							string typeDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);

							yield return new PhoneTypeInfo() {
								PhoneTypeID = phoneTypeID,
								TypeDescription = typeDescr,
							};
						}
					}
				}
			}
		}

		/// <summary>
		///     Method GetPhoneType
		/// </summary>
		/// <param name="TypeID"></param>
		/// <returns></returns>
		public Models.PhoneTypeInfo GetPhoneType(int TypeID) {

			PhoneTypeInfo retVal = null;

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {

					var cmd = new SqlCommand(GetPhoneTypeSQL, conn);
					cmd.Parameters.Add(new SqlParameter { ParameterName = "TypeID", Value = TypeID });
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i;
						if (reader.Read()) {
							i = -1;
							int phoneTypeID = reader.GetInt32(++i);
							string typeDescr = reader.IsDBNull(++i) ? null : reader.GetString(i);

							retVal = new PhoneTypeInfo() {
								PhoneTypeID = phoneTypeID,
								TypeDescription = typeDescr,
							};
						}
					}
				}
			}
			return retVal;
		}

		/// <summary>
		///     Method GetUserList
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Models.UserInfo> GetUserList() {

			ResetMessages();

			using (var conn = ConfigUtils.GetConnection(CONN_NAME)) {
				try {
					conn.Open();
				} catch (SqlException e) {
					MessageID = e.Number;
					MessageString = e.Message;
				} catch (Exception e) {
					MessageID = e.HResult;
					MessageString = e.Message;
				}

				if (conn.State == ConnectionState.Open) {

					var cmd = new SqlCommand(GetUserListSQL, conn);
					using (SqlDataReader reader = cmd.ExecuteReader()) {
						int i;
						while (reader.Read()) {
							i = -1;
							int userID = reader.GetInt32(++i);
							string userName = reader.IsDBNull(++i) ? null : reader.GetString(i);
							int? empID = reader.IsDBNull(++i) ? (int?)null : reader.GetInt32(i);
							int? roleID = reader.IsDBNull(++i) ? (int?)null : reader.GetInt32(i);
							string roleName = reader.IsDBNull(++i) ? null : reader.GetString(i);

							yield return new UserInfo() {
								UserID = userID,
								UserName = userName,
								EmpID = empID,
								RoleID = roleID,
								RoleName = roleName
							};
						}
					}
				}
			}
		}


		#endregion
		#region "Private Methods"
		// ----------------------------------------------------------------------------------------------------

		private void ResetMessages() {
			MessageID = 0;
			MessageString = string.Empty;
			RecordCount = -1;
		}


		#endregion

	}
}
