using System.Linq;
using System.Web.Mvc;
using Validation;
using VABEntLib6Playground.Models;

namespace VABEntLib6Playground.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {            
            return View(new UxModel());
        }

        [HttpPost]
        public ActionResult Index(UxModel model)
        {
            /*var validationResult = ValidationManager.Validate(model);
            
            if (validationResult.Any())
                validationResult.ToList().ForEach(item => ModelState.AddModelError(item.Key, item.Message));
            */
            if (ModelState.IsValid)
                return RedirectToAction("Success");

            return View(model);
        }        

        public ActionResult Success()
        {
            return View();
        }
    }
}
