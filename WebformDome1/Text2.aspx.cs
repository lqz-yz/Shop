using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformDome1
{
    public partial class Text2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //接受参数
          string id=Request.QueryString["id"];
            string name = Session["name"].ToString();//会话程序
             name=Request.Cookies["name"].Value;
            string age=Application["age"].ToString();//应用程序级别容器=全局容器
            //Application最常用场景：统计网站在线人数
            Response.Write(id);
        }
    }
}