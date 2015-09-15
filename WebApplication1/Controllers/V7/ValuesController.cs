using System.Collections.Generic;

namespace WebApplication1.Controllers.V7
{
    public class ValuesController : V6.ValuesController
    {
        public override IEnumerable<string> Get()
        {
            return new string[]
            {
                "WebApplication1.Controllers.V7",
                "WebApplication1.Controllers.V7"
            };
        } 
    }
}
