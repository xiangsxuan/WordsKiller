using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class registerHandle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write("接受到的用户名:"+Request["userName"].ToString()+"密码:"+ Request["userPwd"].ToString());
        SqlData da = new SqlData();
        string sqlString = "update people set userPwd='"+ Request["userPwd"].ToString()+"' where userEmail='"+ Request["userEmail"].ToString() + "'";
        //Response.Write("1111");
        bool reseted = da.ExceSQL(sqlString);
        //Response.Write("2222");
        if (reseted)
        {
            // Response.Write(Request.Form["xingbie"].ToString());
            Response.Write("<script language=javascript>alert('密码修改成功!请重新登陆！');location='Login.aspx';</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('密码修改失败！请联系管理员');</script>");
        }

    }
}