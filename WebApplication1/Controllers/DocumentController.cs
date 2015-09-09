using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace WebApplication1.Controllers
{
    public class DocumentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Upload Docunment";

            return View();
        }

        [HttpGet]
        public ActionResult Image()
        {
            ViewBag.Title = "Upload Images";

            return View();
        }
    }

    public class FileUploadController : Controller
    {
        [HttpPost]
        public bool Index()
        {
            var Form = Request.Form;
            var Files = Request.Files; // Load File collection into HttpFileCollection variable.
            var arr1 = Files.AllKeys;  // This will get names of all files into a string array. 
            for (var loop1 = 0; loop1 < arr1.Length; loop1++)
            {
                //Response.Write("File: " + Server.HtmlEncode(arr1[loop1]) + "<br />");
                //Response.Write("File: " + Server.HtmlEncode(Form.Get("name")) + "<br />");
                //Response.Write("  size = " + Files[loop1].ContentLength + "<br />");
                //Response.Write("  content type = " + Files[loop1].ContentType + "<br />");
                var httpPostedFileBase = Files[loop1];
                if (httpPostedFileBase != null)
                {
                    string savepath = Server.MapPath(string.Format(@"~\App_Data\Upload\{0}", Form.Get("name")));
                    httpPostedFileBase.SaveAs(savepath);
                }
            }
            return true;
        }
    }
}
