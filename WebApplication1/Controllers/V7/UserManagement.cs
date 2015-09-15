using System;
using System.Net;
using System.Net.Http;
using WebApplication1.Controllers.V6;

namespace WebApplication1.Controllers.V7
{
    public class UserManagementController : V6.UserManagementController
    {
        [System.Web.Http.HttpGet]
        public override HttpResponseMessage GetUserInfo(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new UserInfoDtoV7()
            {
                Id = id,
                Name = UserDictionary[id],
                Token = Guid.NewGuid().ToString(),
                PasswordHash = "Attrackting$2014$",
                X = 1080,
                Y = 1920
            });
        }
    }

    public class UserInfoDtoV7 : UserInfoDtoV6
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}