using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Model.RemoveDupsFromList;

namespace Web.Controller.Controllers
{
    public class RemoveDupsFromListController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            ListOfTestDataNoDups listDataNoDups         = new ListOfTestDataNoDups();
            ListOfTestDataWithDups listDataWithDups     = new ListOfTestDataWithDups();

            var dataNoDups          = listDataNoDups.ListDataNoDups();
            var dataWithDups        = listDataWithDups.ListDataWithDups();

            List<ListOfTestData> joinList = dataNoDups.Select(data =>  new ListOfTestData(data.Name, data.Age)).ToList();
            joinList.AddRange(dataWithDups.Select(data =>    new ListOfTestData(data.Name, data.Age)));

            var listMergedAndDupsRemoved = joinList
                .GroupBy(x => new
                {
                    x.Name,
                    x.Age
                })
                .Select(x => new ListOfTestData
                {
                    Name    = x.Key.Name,
                    Age     = x.Key.Age
                }).ToList();

            ViewBag.Message = listMergedAndDupsRemoved.OrderBy(x=>x.Name).ThenByDescending(x=>x.Age);

            var model = new DisplayNoDupsFromList
            {
                DisplayData = listMergedAndDupsRemoved.OrderBy(x=>x.Name).ThenByDescending(x=>x.Age)
            };
            return View(model);
        }
    }
}
