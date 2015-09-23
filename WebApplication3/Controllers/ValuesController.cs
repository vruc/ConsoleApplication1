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
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            throw new ArgumentNullException("Exception");
            //return "value";
        }

        // POST api/values
        public void Post([FromBody]Latlnt value)
        {
            throw new ArgumentNullException("value");
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
