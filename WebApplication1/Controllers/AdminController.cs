using System;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            throw new ArgumentNullException();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SaveNecklaceAnnotaion([Bind(Prefix = "NecklaceAnnotationDto")]NecklaceAnnotationDto annotation)
        {
            throw new OutOfMemoryException();
        }
    }
}