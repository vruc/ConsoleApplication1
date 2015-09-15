using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers.V1
{
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage CheckStatus()
        {
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }
    }
}