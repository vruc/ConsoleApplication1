using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers.V2
{
    public class LocationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetOverview()
        {
            var locationView = new
            {
                NextUpdateInSeconds = 30,
                Position = "New Field In V2",
                CalculatedOn = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd hh:mm:ss")
            };

            return Request.CreateResponse(HttpStatusCode.OK, locationView);

        }
    }
}