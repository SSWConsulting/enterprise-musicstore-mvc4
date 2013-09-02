using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.Common;
using Northwind.MusicStore.BusinessInterfaces;
using Northwind.MusicStore.DependencyResolver.Web;

namespace Northwind.MusicStore.WebUI.Filters
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        private ILogProvider _log;

        [DependencyInject]
        public ILogProvider Log
        {
            get { return _log; }
            set { _log = value; }
        }

        public LogActionAttribute()
        {
            if (Log == null)
            {
                Log = Container.Get<ILogProvider>();
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            Log.DebugFormat("  Action completed");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Log.InfoFormat("Action: {0}.{1} ({2})",
                            filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                            filterContext.ActionDescriptor.ActionName,
                            filterContext.HttpContext.Request.HttpMethod
                            );
            Log.DebugFormat("{0}.{1} {2}{3}",
                            filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                            filterContext.ActionDescriptor.ActionName,
                            GetParameters(filterContext.ActionParameters),
                            GetParameters(filterContext.HttpContext.Request.Form)
                            );

        }

        private string GetParameters(IDictionary<string, object> param)
        {
            var result = param.Any()
                             ? "Params: [" + string.Join(";", param.Keys.Select(key => key + ": " + param[key])) + "]"
                             : "No params";
            return result;
        }

        private string GetParameters(NameValueCollection param)
        {
            var result = param.AllKeys.Any()
                             ? " | Form: [" + string.Join(";", param.AllKeys.Select(key => key + ": " + param[key])) + "]"
                             : " | No form";
            return result;
        }
    }

}