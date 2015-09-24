using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new [] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id");
            }   
            return "value " + id;
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] Latlnt value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return Request.CreateResponse(HttpStatusCode.OK, new {Created = DateTime.Now}, "application/json");
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Latlnt value)
        {
            throw new ArgumentNullException("value");
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class Latlnt
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
