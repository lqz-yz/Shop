using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shop.Models
{
    public class LoginCheckAttribute: AuthorizeAttribute
    {
        //public override void OnAuthorization(AuthorizationContext filterContext) {
        //    //检测Controller上是否包含AllowAnonymous
        //    if (filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Any()) {
        //        return;
        //    }
        //    //判断session容器中是否包含用户信息,如果包含继续执行，否则跳转到登录页
        //    if (filterContext.HttpContext.Session["user"] == null) {
        //        //跳转到登录页
        //        //RedirectResult:重定向
        //        filterContext.Result = new RedirectResult("/Login/Index");
        //    }
        //    // base.OnAuthorization(filterContext);
        //}
        /// <summary>
        /// 判断验证是否通过
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return httpContext.Session["user"] != null;
            return httpContext.Request.Cookies[FormsAuthentication.FormsCookieName] != null && httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value != null && httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value != "";
        }
        /// <summary>
        /// 判断验证未通过
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Login/Index");
        }

    }
}