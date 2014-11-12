using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;


namespace Headspring.Employee.WebServices {

	public class EmpService : IEmpService {

		/// <summary>
		///		Method GetEmployeeList
		/// </summary>
		/// <returns></returns>
		public List<Models.EmployeeInfo> GetEmployeeList() {

			IEnumerable<Models.EmployeeInfo> retVal;

			var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
			retVal = es.GetEmployeeList();
			if (es.MessageID != 0) {
				throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
									HttpStatusCode.NotFound);
			} else if (retVal.Count() == 0) {
				throw new WebFaultException<StatusInfo>(new StatusInfo("No Employees were found", 100),
										HttpStatusCode.NotFound);
			}

			return retVal.ToList();
		}

		/// <summary>
		///		Method GetEmployee
		/// </summary>
		/// <param name="EmpID"></param>
		/// <returns></returns>
		public Models.EmployeeInfo GetEmployee(string EmpID) {

			int empID;
			Models.EmployeeInfo retVal = null;

			if (Int32.TryParse(EmpID, out empID)) {
				var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
				retVal = es.GetEmployee(empID);
				if (es.MessageID != 0) {
					throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
										HttpStatusCode.NotFound);
				} else if (retVal == null) {
					throw new WebFaultException<StatusInfo>(new StatusInfo("Employee " + EmpID + " was not found", 100),
											HttpStatusCode.NotFound);
				}
			}

			return retVal;
		}

		/// <summary>
		///		Mthod GetLocationList
		/// </summary>
		/// <returns></returns>
		public List<Models.LocationInfo> GetLocationList() {

			IEnumerable<Models.LocationInfo> retVal;

			var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
			retVal = es.GetLocationList();
			if (es.MessageID != 0) {
				throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
									HttpStatusCode.NotFound);
			} else if (retVal.Count() == 0) {
				throw new WebFaultException<StatusInfo>(new StatusInfo("No Locations were found", 100),
										HttpStatusCode.NotFound);
			}

			return retVal.ToList();
		}

		/// <summary>
		///		Method GetLocation
		/// </summary>
		/// <param name="LocID"></param>
		/// <returns></returns>
		public Models.LocationInfo GetLocation(string LocID) {

			int locID;
			Models.LocationInfo retVal = null;

			if (Int32.TryParse(LocID, out locID)) {
				var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
				retVal = es.GetLocation(locID);
				if (es.MessageID != 0) {
					throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
										HttpStatusCode.NotFound);
				} else if (retVal == null) {
					throw new WebFaultException<StatusInfo>(new StatusInfo("Location " + LocID + " was not found", 100),
											HttpStatusCode.NotFound);
				}
			}

			return retVal;
		}

		/// <summary>
		///		Method GetJobList
		/// </summary>
		/// <returns></returns>
		public List<Models.JobInfo> GetJobList() {

			IEnumerable<Models.JobInfo> retVal;

			var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
			try {
				retVal = es.GetJobList();
				if (es.MessageID != 0) {
					throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
										HttpStatusCode.NotFound);
				} else if (retVal.Count() == 0) {
					throw new WebFaultException<StatusInfo>(new StatusInfo("No Jobs were found", 100),
										   HttpStatusCode.NotFound);
				}
			} catch (Exception e) {
				throw new WebFaultException<StatusInfo>(new StatusInfo(e.Message, e.HResult),
										   HttpStatusCode.NotFound);
			}

			return retVal.ToList();
		}

		/// <summary>
		///		Method GetJob
		/// </summary>
		/// <param name="JobID"></param>
		/// <returns></returns>
		public Models.JobInfo GetJob(string JobID) {

			int jobID;
			Models.JobInfo retVal = null;

			if (Int32.TryParse(JobID, out jobID)) {
				var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
				retVal = es.GetJob(jobID);
				if (es.MessageID != 0) {
					throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
										HttpStatusCode.NotFound);
				} else if (retVal == null) {
					throw new WebFaultException<StatusInfo>(new StatusInfo("Job " + JobID + " was not found", 100),
											HttpStatusCode.NotFound);
				}
			}

			return retVal;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="EmpID"></param>
		/// <returns></returns>
		public List<Models.PhoneInfo> GetEmployeePhones(string EmpID) {

			int empID;
			IEnumerable<Models.PhoneInfo> retVal = null;

			if (Int32.TryParse(EmpID, out empID)) {
				var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
				retVal = es.GetEmployeePhones(empID);
				if (es.MessageID != 0) {
					throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
										HttpStatusCode.NotFound);
				} else if (retVal == null) {
					throw new WebFaultException<StatusInfo>(new StatusInfo("Employee " + EmpID + " has no phones listed", 100),
											HttpStatusCode.NotFound);
				}
			}

			return retVal.ToList();
		}

		/// <summary>
		///		Method GetPhoneTypeList
		/// </summary>
		/// <returns></returns>
		public List<Models.PhoneTypeInfo> GetPhoneTypeList() {

			IEnumerable<Models.PhoneTypeInfo> retVal;

			var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
			retVal = es.GetPhoneTypeList();
			if (es.MessageID != 0) {
				throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
									HttpStatusCode.NotFound);
			} else if (retVal.Count() == 0) {
				throw new WebFaultException<StatusInfo>(new StatusInfo("No Phone Types were found", 100),
										HttpStatusCode.NotFound);
			}

			return retVal.ToList();
		}

		/// <summary>
		///		Method GetPhoneType
		/// </summary>
		/// <param name="TypeID"></param>
		/// <returns></returns>
		public Models.PhoneTypeInfo GetPhoneType(string TypeID) {

			int typeID;
			Models.PhoneTypeInfo retVal = null;

			if (Int32.TryParse(TypeID, out typeID)) {
				var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
				retVal = es.GetPhoneType(typeID);
				if (es.MessageID != 0) {
					throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
										HttpStatusCode.NotFound);
				} else if (retVal == null) {
					throw new WebFaultException<StatusInfo>(new StatusInfo("Phone Type " + TypeID + " was not found", 100),
											HttpStatusCode.NotFound);
				}
			}

			return retVal;
		}

		/// <summary>
		///		Method GetUserList
		/// </summary>
		/// <returns></returns>
		public List<Models.UserInfo> GetUserList() {

			IEnumerable<Models.UserInfo> retVal;

			var es = DAL.EmployeeStoreFactory.GetEmployeeStore();
			retVal = es.GetUserList();
			if (es.MessageID != 0) {
				throw new WebFaultException<StatusInfo>(new StatusInfo(es.MessageString, es.MessageID),
									HttpStatusCode.NotFound);
			} else if (retVal.Count() == 0) {
				throw new WebFaultException<StatusInfo>(new StatusInfo("No Users were found", 100),
										HttpStatusCode.NotFound);
			}

			return retVal.ToList();
		}
	}
}
