using System;
using System.Net;
using System.Net.Http;
using WebApplication1.Controllers.V5;

namespace WebApplication1.Controllers.V6
{
    public class UserManagementController : V5.UserManagementController
    {
        [System.Web.Http.HttpGet]
        public override HttpResponseMessage GetUserInfo(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new UserInfoDtoV6()
            {
                Id = id,
                Name = UserDictionary[id],
                Token = Guid.NewGuid().ToString(), 
                PasswordHash = "Attrackting$2014$"
            });
        }

        public override HttpResponseMessage HealthCheck()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "THIS HEALTHCHECK IN VERSION 6");
        }
    }

    public class UserInfoDtoV6 : UserInfoDto
    {
        public string PasswordHash { get; set; }
    }
}