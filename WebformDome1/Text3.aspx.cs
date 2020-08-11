using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformDome1
{
    public partial class Text3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //返回json
            string json = "{\"name\":\"lqz\"}";
            Response.Write(json);
        }
    }
}