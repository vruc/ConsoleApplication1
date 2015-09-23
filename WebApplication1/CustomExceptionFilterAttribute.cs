using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using WebGrease.Css.Extensions;

namespace WebApplication1
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var message = new HttpResponseMessage();

            if (context.Exception is FormatException)
            {
                message.StatusCode = HttpStatusCode.BadRequest;
            }
            else if (context.Exception is InvalidTokenException || context.Exception is CannotAccessNecklaceException)
            {
                message.StatusCode = HttpStatusCode.Unauthorized;
            }
            else if (context.Exception is CatHasNoNecklaceException ||
                     context.Exception is UserOrPasswordNotCorrectException)
            {
                message.StatusCode = HttpStatusCode.NotFound;
            }
            else if (context.Exception is NotImplementedException)
            {
                message.StatusCode = HttpStatusCode.NotImplemented;
            }
            else
            {
                message.StatusCode = HttpStatusCode.InternalServerError;
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            //var smsSender = ServiceManagerFactory.GetInstance().CreateSMSNotificationService();

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} {1}", context.Request.Method.Method, context.Request.RequestUri);
            sb.Append("\r\n");

            context.Request.Headers.ForEach(pair =>
            {
                if (pair.Value != null)
                {
                    sb.Append("\r\n");
                    sb.AppendFormat("{0} : {1}", pair.Key, pair.Value.Aggregate((a, b) => a + "," + b));
                }

            });

            if (context.Request.Method == HttpMethod.Post)
            {
                sb.Append("\r\n");
                sb.Append("\r\n");

                var body = context.Request.Content.ReadAsStringAsync().Result;
                sb.Append(body);

            }

            sb.Append("\r\n");
            sb.Append("\r\n");

            sb.Append(context.Exception.Message);
            sb.Append("\r\n");

            sb.Append(context.Exception.StackTrace);

            //smsSender.SendMessage(
            //    ConfigHelper.WebApiExceptionNotificationARN,
            //    ConfigHelper.WebApiExceptionNotificationAPITitle,
            //    sb.ToString());

            context.Response = message;
            context.Response.Content = new StringContent(sb.ToString());
        }
    }

    #region custom exception

    public class UserOrPasswordNotCorrectException : Exception
    {
    }

    public class CatHasNoNecklaceException : Exception
    {
    }

    public class CannotAccessNecklaceException : Exception
    {
    }

    public class InvalidTokenException : Exception
    {
    }


    #endregion

}