using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers.V3
{
    public class LocationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetOverview([FromBody] Address address)
        {
            var rnd = new Random();

            if (rnd.NextDouble() < 0.5)
            {
                throw new Exception("Random Number Smaller Then 0.5.");
            }

            var locationView = new
            {
                NextUpdateInSeconds = 30,
                Position = "New Field In V3",
                CalculatedOn = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd hh:mm:ss"),
                Address = address
            };

            return Request.CreateResponse(HttpStatusCode.OK, locationView);
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }
}