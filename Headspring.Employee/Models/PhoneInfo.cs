
namespace Headspring.Employee.Models {

	public class PhoneInfo {

		public int EmployeeID { get; set; }
		public bool IsPrimary { get; set; }
		public string PhoneNumber { get; set; }
		public string Extension { get; set; }
		public int? TypeID { get; set; }
		public string TypeDescr { get; set; }
	}
}