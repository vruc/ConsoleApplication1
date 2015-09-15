using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class DelayController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(int id = 0)
        {
            var ms = id * 1000;
            Thread.Sleep(ms);

            return Request.CreateResponse(HttpStatusCode.OK, new ResponseBody()
            {
                args = new Dictionary<string, string>() { { "q", "123" } }
            });
        }
    }

    public class ResponseBody
    {
        public Dictionary<string, string> args { get; set; }
    }
}