using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shop.Controllers
{
    public class BaseController<T, B> : Controller
        where T : BaseModel, new()
        where B : IBaseBLL<T>
    {
        IAdminBLL AdminBll = new AdminBLL();
        public Admin CueeentUser {
            get {
                //获取cookie的值
                var cookieValue = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                //解密
                var ticket = FormsAuthentication.Decrypt(cookieValue);
                //var userName = ticket.Name;
                var userID = Convert.ToInt32(ticket.UserData);
                //根据用户名从数据库中查询用户信息
                var result = AdminBll.Getone(userID);
                return result;
                //获取session的值
                //return(Admin)Session["user"];
            }
        }
        public virtual B Bll { get; set; }
        //B bll = new B();
        // GET: Base
        [HttpGet]
        public virtual ActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int result = Bll.Delete(id);
            return Json(new { state = result > 0 ? true : false });
        }
    }
}