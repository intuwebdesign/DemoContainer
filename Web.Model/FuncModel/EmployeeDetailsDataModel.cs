using System.Collections.Generic;
using System.Linq;

namespace Web.Model.FuncModel
{
    public static class EmployeeDetailsDataModel
    {
        public static List<EmployeeDetailsViewModel> AllEmployees()
        {
            List<EmployeeDetailsViewModel> employee = new List<EmployeeDetailsViewModel>
            {
                new EmployeeDetailsViewModel(1, "Peter", "Parker", "Sales"),
                new EmployeeDetailsViewModel(2, "Peter", "Pan", "Sales"),
                new EmployeeDetailsViewModel(3, "Jemma", "Roberts", "Sales"),
                new EmployeeDetailsViewModel(4, "Lucy", "Jenkins", "Sales")
            };

            return employee;
        }

        public static List<EmployeeSales> AllEmployeesSales()
        {
            List<EmployeeSales> sales = new List<EmployeeSales>
            {
                new EmployeeSales(1, 6156369,2020),
                new EmployeeSales(2, 3156369,2020),
                new EmployeeSales(3, 5156369,2020),
                new EmployeeSales(4, 5636999,2020),
                new EmployeeSales(1, 2156369,2021),
                new EmployeeSales(2, 3156369,2021),
                new EmployeeSales(3, 9156369,2021),
                new EmployeeSales(4, 8636999,2021),
            };

            return sales;
        }
    }

    public static class FuncDisplayEmployeeYearlySalesTotal
    {
        public static string ReturnEmployees(IEnumerable<EmployeeDetailsViewModel> employeeList, IEnumerable<EmployeeSales> salesList, double salesTarget, int year)
        {
            IEnumerable<EmployeeSales> sales = from employeeSalesDetails in salesList
                where employeeSalesDetails.YearlySales >= salesTarget
                      && employeeSalesDetails.Year == year
                select new EmployeeSales()
                {
                    EmployeeId = employeeSalesDetails.EmployeeId,
                    YearlySales = employeeSalesDetails.YearlySales
                };

            string str = sales.Aggregate("", (employeeDetails, employeeSales) => employeeList.Where(employee => employeeSales.EmployeeId == employee.EmployeeId).Aggregate(employeeDetails, (currentYearlySales, employee) => currentYearlySales + $"{employee.EmployeeFirstName} {employee.EmployeeLastName} in department {employee.EmployeeDepartment} made {employeeSales.YearlySales:C};"));

            return str.Remove(str.Length - 1);
        }
    }
}
