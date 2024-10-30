using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace GopetHost.Attributes
{
    public class IpRestrictAttribute : ActionFilterAttribute
    {
        private readonly string _allowedIp;

        public IpRestrictAttribute(string allowedIp)
        {
            _allowedIp = allowedIp;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
            if (remoteIp == null || remoteIp.ToString() != _allowedIp)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            base.OnActionExecuting(context);
        }
    }
}
