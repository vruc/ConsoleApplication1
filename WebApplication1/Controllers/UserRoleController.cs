using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class UserRoleController : ApiController
    {
        // GET api/admin/list
        [HttpGet]
        public IEnumerable<string> List()
        {
            return new string[] { "admin", "user" };
        }

        [HttpGet]
        public IEnumerable<string> List(int times)
        {
            var str = Guid.NewGuid().ToString();
            var list = new List<string>();
            while (times > 0)
            {
                list.Add(str);
                times--;
            }
            return list.AsEnumerable();
        }

        [HttpGet]
        public IEnumerable<string> RndGuid()
        {
            return new string[]
            {
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString()
            };
        }
    }
}