using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using System.Threading;

namespace Leonni.Core.Routing
{
    public class AutoLocalizingRoute : Route
    {
        public AutoLocalizingRoute(string url, object defaults, object constraints)
            : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), new MvcRouteHandler()) { }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            // only set the culture if it's not present in the values dictionary yet
            // this check ensures that we can link to a specific language when we need to (fe: when picking your language)
            if (!values.ContainsKey("language"))
            {
                values["language"] = Thread.CurrentThread.CurrentCulture.ToString();//.TwoLetterISOLanguageName;
            }

            return base.GetVirtualPath(requestContext, values);
        }
    }
}
