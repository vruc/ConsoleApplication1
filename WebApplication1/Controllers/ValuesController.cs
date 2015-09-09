using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[]
            {
                "values 1",
                "values 2"
            };
        } 
    }
}
