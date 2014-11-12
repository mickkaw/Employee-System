using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Headspring.Employee.Models;


namespace Headspring.Employee.WebServices {

	#region "Service Contracts"

	[ServiceContract]
	public interface IEmpService {

		#region "Employee Contracts"
		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetEmployeeList"
		)]
		[OperationContract]
		List<EmployeeInfo> GetEmployeeList();

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetEmployee/{EmpID}"
		)]
		[OperationContract]
		EmployeeInfo GetEmployee(string EmpID);


		#endregion
		#region "Location Contracts"

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetLocationList"
		)]
		[OperationContract]
		List<LocationInfo> GetLocationList();

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetLocation/{LocID}"
		)]
		[OperationContract]
		LocationInfo GetLocation(string LocID);


		#endregion
		#region "Job Contracts"

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetJobList"
		)]
		[OperationContract]
		List<JobInfo> GetJobList();

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetJob/{JobID}"
		)]
		[OperationContract]
		JobInfo GetJob(string JobID);


		#endregion
		#region "Phone Contracts"

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetEmployeePhones/{EmpID}"
		)]
		[OperationContract]
		List<PhoneInfo> GetEmployeePhones(string EmpID);


		#endregion
		#region "Phone Type Contracts"

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetPhoneTypeList"
		)]
		[OperationContract]
		List<PhoneTypeInfo> GetPhoneTypeList();

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetPhoneType/{TypeID}"
		)]
		[OperationContract]
		PhoneTypeInfo GetPhoneType(string TypeID);


		#endregion
		#region "User Contracts"

		[WebGet(
			BodyStyle = WebMessageBodyStyle.Wrapped
			, ResponseFormat = WebMessageFormat.Json
			, RequestFormat = WebMessageFormat.Xml
			, UriTemplate = "/GetUserList"
		)]
		[OperationContract]
		List<UserInfo> GetUserList();


		#endregion
	}

	[DataContract]
	public class StatusInfo {

		public StatusInfo(string messageString, int messageID) {
			MessageString = messageString;
			MessageID = messageID;
		}

		[DataMember]
		public string MessageString { get; private set; }

		[DataMember]
		public int MessageID { get; private set; }
	}


	#endregion
}
