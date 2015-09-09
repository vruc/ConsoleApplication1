using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            var form = Request.Form;
            var files = Request.Files; // Load File collection into HttpFileCollection variable.
            var arr1 = files.AllKeys; // This will get names of all files into a string array. 
            for (var loop1 = 0; loop1 < arr1.Length; loop1++)
            {
                //Response.Write("File: " + Server.HtmlEncode(arr1[loop1]) + "<br />");
                //Response.Write("File: " + Server.HtmlEncode(Form.Get("name")) + "<br />");
                //Response.Write("  size = " + Files[loop1].ContentLength + "<br />");
                //Response.Write("  content type = " + Files[loop1].ContentType + "<br />");
                var httpPostedFileBase = files[loop1];
                if (httpPostedFileBase != null)
                {
                    var name = form.Get("name");
                    var path = Server.MapPath(string.Format(@"~\App_Data\upload\{0}", name));
                    httpPostedFileBase.SaveAs(path);
                    ThumbnailImage(path, name, 100, 200, 300, 500, 1000);
                }
            }
            return true;
        }

        private void ThumbnailImage(string sourcePath, string name, params float[] sizes)
        {

            if (!System.IO.File.Exists(sourcePath)) return;

            const string path = @"~\App_Data\thumbnail\{0}";

            sizes.ForEach(width =>
            {
                using (var fs = new FileStream(sourcePath, FileMode.Open))
                {
                    var img = new WebImage(fs);
                    img.Resize((int) width, (int) (img.Height*width/img.Width));
                    img.Save(string.Format(path, name.Replace(".", "-"+width + ".")));
                }
            });
        }
    }

    public class ViewImageController : Controller
    {
        public ActionResult Index(string name)
        {
            var path = Server.MapPath(string.Format(@"~\App_Data\Upload\{0}", name));
            if (System.IO.File.Exists(path))
            {
                return base.File(path, "image/jpeg");
            }
            return View("Error");
        }
    }


}
