using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers.V5
{
    public class UserManagementController : ApiController
    {
        protected static readonly Dictionary<int, string> UserDictionary = new Dictionary<int, string>()
        {
            {1, "Antony Su"},
            {2, "Gavin Mok"},
            {3, "Gilly Chen"},
            {4, "Kenny Li"},
            {5, "Marco Savini"},
            {6, "Oscar Chen"},
        };

        [System.Web.Http.HttpGet]
        public virtual HttpResponseMessage GetUserInfo(int id)
        {
            if (UserDictionary.ContainsKey(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new UserInfoDto()
                {
                    Id = id,
                    Name = UserDictionary[id],
                    Token = Guid.NewGuid().ToString()
                });   
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "Resources NotFound");
        }

        [System.Web.Http.HttpGet]
        public virtual HttpResponseMessage HealthCheck()
        {
            return Request.CreateResponse(HttpStatusCode.OK, true);
        }

        [System.Web.Http.HttpPost]
        public virtual bool UpdatePassword([FromBody] ChangePasswordDto newPassaord)
        {
            return true;
        }

    }

    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }

    public class ChangePasswordDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}