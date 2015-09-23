using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class UploadController : ApiController
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        public async Task<HttpResponseMessage> PostFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var list = new List<String>();

                foreach (MultipartFileData file in provider.FileData)
                {
                    var ret = string.Format("{0}: {1}", file.Headers.ContentDisposition.FileName, new FileInfo(file.LocalFileName).Length);
                    list.Add(ret);
                    Log.Info(ret);
                }

                var formData = provider.FormData;
                foreach (string key in formData.AllKeys)
                {
                    foreach (var val in formData.GetValues(key))
                    {
                        var ret = string.Format("{0}: {1}", key, val);
                        list.Add(ret);
                        Log.Info(ret);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}