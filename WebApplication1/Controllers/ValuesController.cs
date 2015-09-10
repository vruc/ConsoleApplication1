using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[]
            {
                "values 1",
                "values 2"
            };
        } 
    }


    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult SaveNecklaceAnnotaion([Bind(Prefix = "NecklaceAnnotationDto")]NecklaceAnnotationDto annotation)
        {
            if (ModelState.IsValid)
            {
                annotation.NecklaceId = 999;
            }
            return View(annotation);
        }
    }

    public class NecklaceAnnotationDto
    {
        public int NecklaceId { get; set; }
        public string Annotation { get; set; }
    }
}
