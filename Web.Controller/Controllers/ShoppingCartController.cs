using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Web.Model.ShoppingCartModel;

namespace Web.Controller.Controllers
{
    public class ShoppingCartController : System.Web.Mvc.Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var dataModel = new ListOfProductsDataModel(); //This would be from your database etc

            var model = new DisplayProductsViewModel
            {
                DisplayProducts = dataModel.HardCodeListOfProducts()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(CustomerShippingDetailsViewModel model)
        {
            var jsonIds = JsonConvert.DeserializeObject<List<ProductIds>>(model.ProductIds);
            if (jsonIds != null && jsonIds.Count == 0)
            {
                ModelState.AddModelError("", "No Products Purchased.");
            }

            if (ModelState.IsValid)
            {
                //Only need the product ID as can now get the default value for the price from server based on ID
                //This helps prevent the clientside values been modified
                var dataModel = new ListOfProductsDataModel();
                {
                    List<ProductIds> totalPrice = new List<ProductIds>();

                    totalPrice.AddRange(from id in jsonIds
                                        select id.Id.Remove(0, 10) //Here remove the ProductId- from start of string
                                        into productId
                                        from price
                                            in dataModel.HardCodeListOfProducts().ToList()
                                        where price.ProductId == productId
                                        select new ProductIds(price.ProductTitle, price.ProductPrice));

                    double sumOfTotalPrice = totalPrice.Select(x => x.ProductPrice).Sum();

                    //You would now log the total price and customer details to your database
                    //Inform the customer payment was a success
                    //And if required send an email

                    if (Request.IsAjaxRequest())
                    {
                        return Json(new { Confirm = true, isJsonErrorList = false, message = $"<p class=\"alert alert-success\">Hi {model.CustomerFirstName}, Thank you for your payment of {sumOfTotalPrice:C}</p>" }, JsonRequestBehavior.AllowGet);
                    }

                    TempData["ServerMessage"] = $"Hi {model.CustomerFirstName}, Thank you for your payment of {sumOfTotalPrice:C}";
                    return View();
                }
            }

            //Would return any validation errors here

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            List<string> errorList = new List<string>();

            var modelErrors = allErrors as ModelError[] ?? allErrors.ToArray();
            errorList.Add("<p class=\"text-danger\">Please fix the errors below.</p>");
            errorList.Add("<ul class=\"list-unstyled\">");

            errorList.AddRange(modelErrors.Select(error => $"<li class=\"text-danger\">{error.ErrorMessage}</li>"));

            errorList.Add("</ul>");

            string displayErrors = string.Join(" ", errorList.ToArray());

            if (Request.IsAjaxRequest())
            {
                return Json(new { Confirm = false, isJsonErrorList = true, message = displayErrors }, JsonRequestBehavior.AllowGet);
            }

            TempData["ServerMessage"] = displayErrors;
            return View();

        }
    }
}