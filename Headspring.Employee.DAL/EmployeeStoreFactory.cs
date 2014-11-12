
namespace Headspring.Employee.DAL {

	public static class EmployeeStoreFactory {

		public static IEmployeeStore GetEmployeeStore() {
			return new DefaultEmployeeStore();
		}
	}
}
