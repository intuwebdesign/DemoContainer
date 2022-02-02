namespace Web.Model.FuncModel
{
    public class EmployeeDetailsViewModel
    {
        public EmployeeDetailsViewModel(int employeeId, string employeeFirstName, string employeeLastName, string employeeDepartment)
        {
            EmployeeId              = employeeId;
            EmployeeFirstName       = employeeFirstName;
            EmployeeLastName        = employeeLastName;
            EmployeeDepartment      = employeeDepartment;
        }

        public int EmployeeId               { get;}
        public string EmployeeFirstName     { get;}
        public string EmployeeLastName      { get;}
        public string EmployeeDepartment    { get;}
    }

    public class EmployeeSales
    {
        public EmployeeSales(int employeeId, double yearlySales, int year)
        {
            EmployeeId      = employeeId;
            YearlySales     = yearlySales;
            Year            = year;
        }

        public EmployeeSales(){}

        public int EmployeeId       { get; set; }
        public double YearlySales   { get; set; }
        public int Year             { get; set; }
    }
}