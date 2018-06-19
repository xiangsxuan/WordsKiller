using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LogIn(object sender, EventArgs e)
    {
        SqlData da = new SqlData();
        string sqlString = "select userName,userPwd,userEmail from people where userName='" + UserNameUserEmail.Text.Trim() + "' or userEmail='" + UserNameUserEmail.Text.Trim() + "'";
        SqlDataReader sqlDataReader = da.ExceRead(sqlString);
        //        Response.Write("1<br>");
        try { 
        if (sqlDataReader.HasRows)
        {
//           Response.Write("2<br>");

            sqlDataReader.Read();
            Boolean isPasswordRight = (sqlDataReader.GetValue(1).ToString().Equals(Password.Text.Trim().GetHashCode().ToString()));
            //Response.Write("Password.Text.Trim().GetHashCode()"+ Password.Text.Trim().GetHashCode());
            //Response.Write("<br>sqlDataReader.GetValue(1).ToString().Trim()" + sqlDataReader.GetValue(1).ToString().Trim() + "<br>");
            if (isPasswordRight)
            {
//                Response.Write("3<br>");
                Session["userName"] = sqlDataReader.GetValue(0);
                Session["userId"] = sqlDataReader.GetValue(2);

                Response.Write("<script language=javascript>alert('登陆成功!');location='Default.aspx';</script>");

            }
            else
            {
//                Response.Write("4<br>");
                Response.Write("<script language=javascript>alert('用户名或密码错误!');");
            }
            // Response.Write(Request.Form["xingbie"].ToString());
        }
        else
        {
//           Response.Write("5<br>");

            Response.Write("<script language=javascript>alert('您还未注册,请进行注册！');location='Register.aspx';</script>");
        }
        }finally
        {
            sqlDataReader.Close();
        }
    }
}