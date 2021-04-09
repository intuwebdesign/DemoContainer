using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Controller.Helpers;

namespace Web.Controller.Controllers
{
    public class PagerController : System.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchResults(string q, int? page = 0)
        {
            if (string.IsNullOrEmpty(q))
            {
                q = "";
            }
            List<ListBeveragesViewModel> model = new List<ListBeveragesViewModel>();

            var beverages = ListOfBeverages.TopAlcoholicDrinksUk();

            foreach (var beverage in beverages.Where(x=>string.Equals(x.Type, q, StringComparison.CurrentCultureIgnoreCase)))
            {
                string beverageName = beverage.Beverage;
                string beverageType = beverage.Type;

                model.Add(new ListBeveragesViewModel(beverageName,beverageType));
            }

            int totalNumberOfRecords = model.Count;

            int numberOfRecordsPerPage = 5;
            var numberOfRecordsToDisplay = new PagerHelper.Pagination(totalNumberOfRecords, page, numberOfRecordsPerPage);

            var displayResults = new DisplayPagerViewModel
            {
                DisplaySearchResults    = model.Skip((numberOfRecordsToDisplay.CurrentPage - 1)* numberOfRecordsToDisplay.PageSize).Take(numberOfRecordsToDisplay.PageSize).ToList(),
                Pager                   = numberOfRecordsToDisplay,
                NumberOfResults         = model.Count
            };
            return View("~/Views/Pager/SearchResults.cshtml",displayResults);
        }
    }
}
