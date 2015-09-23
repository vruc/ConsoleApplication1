using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace WebApplication1
{
    /// <summary>
    /// 
    /// </summary>
    public class NamespaceHttpControllerSelector : IHttpControllerSelector
    {
        private const string DefaultNamespace = "V1";
        private const string NamespaceKey = "version";
        private const string ControllerKey = "controller";

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;
        private readonly HashSet<string> _duplicates;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public NamespaceHttpControllerSelector(HttpConfiguration config)
        {
            _configuration = config;
            _duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);

            IAssembliesResolver assembliesResolver = _configuration.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();

            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);

            foreach (Type t in controllerTypes)
            {
                if (t.Namespace != null)
                {
                    var segments = t.Namespace.Split(Type.Delimiter);

                    var controllerName =
                        t.Name.Remove(t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);

                    var key = String.Format("{0}.{1}", segments[segments.Length - 1].ToUpper(), controllerName);

                    if (dictionary.Keys.Contains(key))
                    {
                        _duplicates.Add(key);
                    }
                    else
                    {
                        dictionary[key] = new HttpControllerDescriptor(_configuration, t.Name, t);
                    }
                }
            }

            foreach (string s in _duplicates)
            {
                dictionary.Remove(s);
            }
            return dictionary;
        }

        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result = null;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T) result;
            }
            return default(T);
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            IHttpRouteData routeData = request.GetRouteData();
            if (null == routeData)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            string nameSpace = GetRouteVariable<string>(routeData, NamespaceKey).ToUpper();
            if (string.IsNullOrEmpty(nameSpace))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string controller = GetRouteVariable<string>(routeData, ControllerKey);
            if (string.IsNullOrEmpty(controller))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            int version = -1;
            int latestVersion = -1;

            Regex reg = new Regex(@"v(?<ver>\d{1,})", RegexOptions.IgnoreCase);
            if (!reg.IsMatch(nameSpace))
            {
                controller = nameSpace;
                nameSpace = DefaultNamespace;
            }
            else
            {
                Match m = reg.Match(nameSpace);
                if (m.Success)
                {
                    int.TryParse(m.Groups["ver"].Value, out version);
                    var val = ConfigurationManager.AppSettings["LatestApiVersion"];
                    if (!string.IsNullOrEmpty(val))
                    {
                        int.TryParse(val, out latestVersion);
                    }

                    if (version > latestVersion)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }

            string key = String.Format("{0}.{1}", nameSpace.ToUpper(), controller);

            HttpControllerDescriptor controllerDescriptor;

            if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            else
            {
                version--;
                while (version > 0)
                {
                    key = String.Format("V{0}.{1}", version, controller);
                    if (_controllers.Value.TryGetValue(key, out controllerDescriptor))
                    {
                        return controllerDescriptor;
                    }
                    version--;
                }

                if ((_duplicates.Contains(key)))
                {
                    throw new HttpResponseException(
                        request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                            "Multiple controllers were found that match this request."));
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

            }
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }
    }
}