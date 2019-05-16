using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JustinandKelley
{
    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String url = filterContext.HttpContext.Request.Url.OriginalString;
            Models.AuditHistory ah = new Models.AuditHistory();
            ah.AuditURL = url;
            Controllers.AuditController aC = new Controllers.AuditController();
            aC.Create(ah);
        }
    }
}
