using System.Collections.Generic;

namespace WebApplication1.Controllers.V6
{
    public class ValuesController : V5.ValuesController
    {
        public override IEnumerable<string> Get()
        {
            return new string[]
            {
                "WebApplication1.Controllers.V6",
                "WebApplication1.Controllers.V6",
                "values 1",
                "values 2"
            };
        } 
    }
}
