using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Model.ReflectionModel;

namespace Web.Controller.Controllers
{
    public class ReflectionController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            //Reflection Single Item
            ReflectionSingleString single = new ReflectionSingleString();
            string name = ReturnSingleString.ReturnReflectionSingleString(single);
            TempData["SingleItem"] = name;



            //Reflection List
            ListOfUsers users = new ListOfUsers();
            var user = users.ReturnUserDetails();
            List<string> reflectonExample = user.Select(ListOfUsers.GetListOfUsers).ToList();

            TempData["ReflectionList"] = reflectonExample;

            return View();
        }
    }
}
