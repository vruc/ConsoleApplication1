using System.Collections.Generic;
using System.Web.Http;

namespace WebApplication1.Controllers.V5
{
    public class ValuesController : ApiController
    {
        public virtual IEnumerable<string> Get()
        {
            return new string[]
            {
                "WebApplication1.Controllers.V1",
                "WebApplication1.Controllers.V1",
                "values 1",
                "values 2"
            };
        }

        public string Get(int id)
        {
            return "WebApplication1.Controllers.V1 ---> [" + id + "]";
        }

    }



}
