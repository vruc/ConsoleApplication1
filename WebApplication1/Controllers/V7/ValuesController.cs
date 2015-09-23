using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

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

    [ApiExplorerSettings(IgnoreApi = true)]

    public class PostmanController : ApiController
    {

        public HttpResponseMessage Get()
        {

            var collection = Configuration.Properties.GetOrAdd("postmanCollection", k =>
            {

                var requestUri = Request.RequestUri;

                string baseUri = requestUri.Scheme + "://" + requestUri.Host + ":" + requestUri.Port + HttpContext.Current.Request.ApplicationPath;

                var postManCollection = new PostmanCollection
                {
                    id = Guid.NewGuid(),
                    name = "KADDZ Web API Service",
                    timestamp = DateTime.Now.Ticks,
                    requests = new Collection<PostmanRequest>()
                };

                Regex re1 = new Regex("V\\d+\\.");
                Regex re2 = new Regex("\\{version\\}");

                foreach (var apiDescription in Configuration.Services.GetApiExplorer().ApiDescriptions)
                {
                    var path = re1.Replace(apiDescription.RelativePath, "");
                    path = re2.Replace(path, "v1");

                    var request = new PostmanRequest
                    {
                        collectionId = postManCollection.id,
                        id = Guid.NewGuid(),
                        method = apiDescription.HttpMethod.Method,
                        url = baseUri.TrimEnd('/') + "/" + path,
                        description = apiDescription.Documentation,
                        name = path,
                        data = "",
                        headers = "Authorization: \nAccept: text/json\nContent-Type: text/json; charset:UTF-8\n",
                        dataMode = "params",
                        timestamp = 0
                    };

                    postManCollection.requests.Add(request);

                }

                return postManCollection;

            }) as PostmanCollection;

            return Request.CreateResponse<PostmanCollection>(HttpStatusCode.OK, collection, "application/json");
        }

    }

    public class PostmanCollection
    {

        public Guid id { get; set; }

        public string name { get; set; }

        public long timestamp { get; set; }

        public Collection<PostmanRequest> requests { get; set; }

    }

    public class PostmanRequest
    {

        public Guid collectionId { get; set; }

        public Guid id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public string method { get; set; }

        public string headers { get; set; }

        public string data { get; set; }

        public string dataMode { get; set; }

        public long timestamp { get; set; }

    }

}
