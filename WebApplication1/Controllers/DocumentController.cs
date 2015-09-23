using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace WebApplication1.Controllers
{
    public class DocumentController : BaseController
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


    public class BaseController : Controller
    {
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private readonly IAuthenticationService _authenticationService = ServiceManagerFactory.GetInstance().CreateAuthenticationService();
        //private readonly ISettingService _settingService = ServiceManagerFactory.GetInstance().CreateSettingService();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var authorization = HttpContext.Request.Headers.Get("Authorization");

            //if (String.IsNullOrEmpty(authorization))
            //{
            //    Log.Error("Request without Authorization header");
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
            //else if (!_authenticationService.ValidateToken(new UserTokenDTO { Token = authorization }))
            //{
            //    Log.Error("Request with invalid Authorization header");
            //    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            
            var context = filterContext.HttpContext;
            var sb = new StringBuilder();

            if (context.Request.Url != null)
            {
                sb.AppendFormat("{0} {1}", context.Request.RequestType, context.Request.Url.PathAndQuery);
                sb.Append("\r\n");
            }
            else
            {
                sb.AppendFormat("{0} {1}", context.Request.RequestType, context.Request.RawUrl);
                sb.Append("\r\n");
            }

            context.Request.Headers.AllKeys.ForEach(key =>
            {
                var val = context.Request.Headers.GetValues(key);
                if (val == null) return;
                sb.Append("\r\n");
                sb.AppendFormat("{0} : {1}", key, val.Aggregate((a, b) => a + ", " + b));
            });


            if (context.Request.RequestType != "GET")
            {
                try
                {
                    sb.Append("\r\n");
                    sb.Append("\r\n");

                    var req = Request.InputStream;
                    req.Seek(0, SeekOrigin.Begin);
                    var json = new StreamReader(req).ReadToEnd();
                    sb.Append(json);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            sb.Append("\r\n");
            sb.Append("\r\n");

            sb.Append(filterContext.Exception.Message);
            sb.Append("\r\n");
            sb.Append("\r\n");

            sb.Append(filterContext.Exception.StackTrace);

            context.Response.StatusCode = 500;

            filterContext.Result = View("Error", new KHandleErrorInfo(filterContext.Exception));

            base.OnException(filterContext);

        }

    }
    public class KHandleErrorInfo : HandleErrorInfo
    {
        public KHandleErrorInfo(Exception exception) : base(exception, " ", " ") { }
    }

}
