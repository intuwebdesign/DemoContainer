using System.Web.Mvc;
using Web.Model.TupleModel;

namespace Web.Controller.Controllers
{
    public class TupleExampleController : System.Web.Mvc.Controller
    {
        public ActionResult Index(bool hasPet = true)
        {
            var tupleValues = DisplayListOfPetsTuple.ReturnTupleExamples(hasPet);

            ViewBag.Name            = tupleValues.Name;
            ViewBag.HasPet          = tupleValues.HasPet;
            ViewBag.ListOfPets      = tupleValues.ListOfPets;

            return View();
        }
    }
}
