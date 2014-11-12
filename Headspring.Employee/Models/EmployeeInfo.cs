
namespace Headspring.Employee.Models {

	public class EmployeeInfo {

		public int EmpID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Email { get; set; }
		public int? JobID { get; set; }
		public int? LocationID { get; set; }
		public string JobCode { get; set; }
		public string JobDescr { get; set; }
		public string LocationDescr { get; set; }
		public string City { get; set; }
		public string ST { get; set; }
	}
}