using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformDome1
{
    public class Like
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    //webform:请求地址页面具体路径
    //MVC:请求地址contooler/action
    public partial class Text1 : System.Web.UI.Page
    {
        //请求服务端后 首先执行页面加载方法,如果有点击事件在执行点击事件相关方法

        protected void Page_Load(object sender, EventArgs e)
        {
            //是否是回发:1.不是第一次请求 2.post请求
            //IsPostBack:当请求是回发时为true,否则为false
            if (!IsPostBack)
            {
                //第一次请求(只要是get请求都算第一次请求)
                LoadData();
            }
            Session["name"] = "lqz";
            Response.Cookies.Add(new HttpCookie("name", "lqz"));
            Application["age"] = 10;
        }

        private void LoadData()
        {
            List<Like> likes = new List<Like>() {
            new Like(){
                ID=1,
                Name="足球"
            },
             new Like(){
                ID=2,
                Name="蓝球"
            },
            new Like()
            {
                ID = 3,
                Name = "拼球"
            },

            };
            //chkLike.Items.Clear();
            foreach (var like in likes)
            {
                ListItem item = new ListItem();
                item.Text = like.Name;
                item.Value = like.ID.ToString();
                chkLike.Items.Add(item);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //获取文本框的值
            string name = txtName.Text;
            //获取下拉框的值
            string sex = drpSex.SelectedValue;
            //获取CheckBoxList的值
            string likes = "";
            foreach (ListItem item in chkLike.Items)
            {
                if (item.Selected)
                {
                    likes+= item.Value+",";
                }
            }
            //获取RadioButtonList的值
            string eduation = rdoEduation.SelectedValue;

            //获取CheckBox是否选中
            bool isDy = chkIsDy.Checked;

            //后太跳转,Redirect重定向 要写相对地址
            Response.Redirect("text2.aspx?id=1");
            //Response.Write(name);
        }
    }
}