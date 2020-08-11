<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Text1.aspx.cs" Inherits="WebformDome1.Text1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <a href="Text2.aspx?id=1">跳转到text2</a>
    <form id="form1" runat="server">
        <div>
            <%--<input id="txtName" name="txtName" type="text" />--%>
            姓名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
       <div>
            性别:<asp:DropDownList ID="drpSex" runat="server">
                <asp:ListItem Text="男" Value="1"></asp:ListItem>
                 <asp:ListItem Text="女" Value="0"></asp:ListItem>
               </asp:DropDownList>
        </div>
          <div>
            爱好:<asp:CheckBoxList ID="chkLike" runat="server">
                <%-- <asp:ListItem Text="足球" Value="1"></asp:ListItem>
                 <asp:ListItem Text="篮球" Value="2"></asp:ListItem>
                <asp:ListItem Text="茶艺" Value="3"></asp:ListItem>--%>
               </asp:CheckBoxList>
        </div>
        <div>
            学历:<asp:RadioButtonList ID="rdoEduation" runat="server">
                  <asp:ListItem Text="小学" Value="1"></asp:ListItem>
                 <asp:ListItem Text="大专" Value="2"></asp:ListItem>
                <asp:ListItem Text="初中" Value="3"></asp:ListItem>
                <asp:ListItem Text="高中" Value="4"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            是否党员:<asp:CheckBox ID="chkIsDy" runat="server" Checked="false"/>
        </div>
        <asp:Button ID="Button1" runat="server" Text="测试" Height="21px" OnClick="Button1_Click" />
    </form>
</body>
</html>
