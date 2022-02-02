using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Model.FuncModel;

namespace Web.Controller.Controllers
{
    public class FuncController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            Func<List<EmployeeDetailsViewModel>, List<EmployeeSales>, double, int, string> funcYearlySales = FuncDisplayEmployeeYearlySalesTotal.ReturnEmployees;

            string res = funcYearlySales(EmployeeDetailsDataModel.AllEmployees(), EmployeeDetailsDataModel.AllEmployeesSales(), 5000000, 2021);

            string[] splitRes = res.Split(';');

            List<string> emp = new List<string>();

            foreach (var employee in splitRes)
            {
                emp.Add(employee);
            }

            TempData["Employees"] = emp;

            return View();
        }
    }
}