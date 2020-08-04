using COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using IBLL;
using System.Web.Security;

namespace Shop.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        IAdminBLL Bll = new AdminBLL();
        // GET: Login
        public ActionResult Index()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null && Request.Cookies[FormsAuthentication.FormsCookieName].Value != null && Request.Cookies[FormsAuthentication.FormsCookieName].Value != "")
            {
                var cookieValue = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                var ID = Convert.ToInt32(FormsAuthentication.Decrypt(cookieValue).UserData);
                //根据用户名从数据库中查询用户信息
                var result = Bll.Getone(ID);

                if (result != null && result.ID == ID)
                {
                    //Session["user"] = result[0];
                    //下面两句实现滑动过期时间
                    Response.Cookies[FormsAuthentication.FormsCookieName].Value = cookieValue;
                    Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(3);
                    return Redirect("/Product/List");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Name, string Password)
        {
            //正常逻辑 :根据用户名和密码从数据库中查询用户是否存在.如果存在将用户信息写入session容器
            //一般存用户实体对象
            //Session["user"] = 1;
            string jiayan = "@#$%&*";
            Password = Md5Helper.Md5(Md5Helper.Md5(jiayan + Password));//Md5不可逆
            //var result = Bll.Search(x => x.Name == Name && x.Password == Password).ToList();
            //if (result.Count == 1) {
            //    Session["user"] = Name + Password;
            //    return Redirect("/product/List");
            //}
            //else {
            //    return Json(new { state = false, msg = "账号或密码错误" });
            //}
            ////Session["user"] = null;//清空session容器中的用户信息
            //return Redirect("/product/List");
            //cookie:用户ID或token
            //声明一个cookie对象
            //HttpCookie cookie = new HttpCookie("user");
            //cookie.Value = "1";
            //cookie如果不设置有效期,浏览器关闭就会失效
            var result = Bll.Search(x => x.Name == Name && x.Password == Password).ToList();
            if (result.Count == 1)
            {
                FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(
                 1,//版本
                 FormsAuthentication.FormsCookieName,//cookie名称
                 DateTime.Now,//发布时间
                 DateTime.Now.AddMinutes(40),//过期时间
                 false,//持久性  一般为false
                 result[0].ID.ToString()
                 );
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Ticket));

                Response.Cookies.Add(cookie);

                return Redirect("/product/List");
            }
            else
            {
                return Json(new { state = false, msg = "账号或密码错误" });
            }
            
        }
        public ActionResult Logot()
        {
            //Session["user"] = null;//清空session容器中的用户信息
            Response.Cookies[FormsAuthentication.FormsCookieName].Value = "";
            return Redirect("/Login/index");
        }
    }
}