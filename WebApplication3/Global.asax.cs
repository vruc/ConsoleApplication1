using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication3
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.MessageHandlers.Add(new ExceptionHandler());
        }
    }


    public class ExceptionHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var requestHeaders = request.ToString();
            var requestBody = request.Content.ReadAsStringAsync().Result;

            var fullRequest = requestHeaders + "\r\n\r\n" + requestBody + "\r\n\r\n";

            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;

                    if (task.Result.StatusCode == HttpStatusCode.InternalServerError || task.Result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var responseHeaders = task.Result.ToString();
                        var responseBody = task.Result.Content.ReadAsStringAsync().Result;

                        var fullResponse = responseHeaders + "\r\n\r\n" + responseBody;

                        response.Content = new StringContent(fullRequest + fullResponse);
                    }

                    return response;

                }, cancellationToken);
        }
    }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var message = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            message.Content = new StringContent(
                string.Format("{0} \r\n {1}", actionExecutedContext.Exception.Message,
                    actionExecutedContext.Exception.StackTrace));

            actionExecutedContext.Response = message;

            base.OnException(actionExecutedContext);
        }
    }

}
