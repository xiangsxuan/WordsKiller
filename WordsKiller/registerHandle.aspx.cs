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
        string sqlString = "INSERT INTO people(userName,userPwd,userEmail)";
        sqlString += " VALUES('" + Request["userName"].ToString() + "','" + Request["userPwd"].ToString() +"','"+ Request["userEmail"].ToString() + "')";
        //Response.Write("1111");
        bool added = da.ExceSQL(sqlString);
        //Response.Write("2222");
        if (added)
        {
            // Response.Write(Request.Form["xingbie"].ToString());
            Response.Write("<script language=javascript>alert('注册成功!您可以登陆了！');location='Login.aspx';</script>");
        }
        else
        {
            Response.Write("<script language=javascript>alert('注册失败！');</script>");
        }

    }
}