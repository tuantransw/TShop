//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;

//namespace TShop.Controllers
//{
//    public class BaseController : Controller
//    {
//        // GET: Base
//       protected override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            var sess = Session["TaiKhoanAdmin"];
//            if(sess == null)
//            {
//                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "DangNhapAdmin", Action = "Index" }));
//            }
//            base.OnActionExecuting(filterContext);
//        }
//    }
//}