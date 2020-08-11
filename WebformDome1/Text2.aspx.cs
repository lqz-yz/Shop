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
            Response.Write(id);
        }
    }
}