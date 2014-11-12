using System.Collections.Generic;
using Headspring.Employee.Models;

namespace Headspring.Employee.DAL {

	public interface IEmployeeStore {

		int MessageID { get; set; }
		string MessageString { get; set; }

		IEnumerable<EmployeeInfo> GetEmployeeList();
		EmployeeInfo GetEmployee(int EmpID);

		IEnumerable<LocationInfo> GetLocationList();
		LocationInfo GetLocation(int LocID);

		IEnumerable<JobInfo> GetJobList();
		JobInfo GetJob(int JobID);

		IEnumerable<PhoneTypeInfo> GetPhoneTypeList();
		PhoneTypeInfo GetPhoneType(int TypeID);

		IEnumerable<PhoneInfo> GetEmployeePhones(int EmpID);

		IEnumerable<UserInfo> GetUserList();
	}
}
