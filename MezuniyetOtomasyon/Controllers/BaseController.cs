using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MezuniyetOtomasyon.Controllers
{
    public class BaseController : Controller
    {

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (Request.Cookies["_userDetails"] != null)
            {
                HttpCookie c = Request.Cookies["_userDetails"];
                Session.Add("_userID", c.Values["_userID"]);
                Session.Add("_userName", c.Values["_userName"]);
                Session.Add("_userFullName", c.Values["_userFullName"]);
                Session.Add("_userUnits", c.Values["_userUnits"]);
            }
            return base.BeginExecuteCore(callback, state);
        }

        public class KullaniciDogrulama : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                base.OnActionExecuting(context);

                if (context.HttpContext.Session["_userID"] == null)
                {
                    if (context.HttpContext.Request.IsAjaxRequest())
                    {
                        context.Result = new JavaScriptResult()
                        {
                            Script = "window.location='/Kullanici/Giris/';"
                        };
                    }
                    else
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Kullanici",
                            action = "Giris"
                        }));
                    }
                }
            }
        }

    }
}