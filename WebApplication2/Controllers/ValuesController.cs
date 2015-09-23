using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody] Address value)
        {
            //throw new ArgumentException(value);
            return Request.CreateResponse(HttpStatusCode.OK,
                new Latlnt
                {
                    Latidude = new Random().NextDouble() * 180,
                    Lontitude = new Random().NextDouble() * 90
                });
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string StreetNum { get; set; }
    }

    public class Latlnt
    {
        public double Latidude { get; set; }
        public double Lontitude { get; set; }
    }
}
