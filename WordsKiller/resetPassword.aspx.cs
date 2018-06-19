using System;
using System.Web.Providers.Entities;
using System.Web.UI;

public partial class AccountRegister : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        string userName = Session["userName"].ToString();
        String userNameUserPwdUserEmail = "userName=" + userName +
                                          "&userPwd=" + Password.Text.Trim().GetHashCode() +
                                          "&userEmail=" + userEmail.Text.Trim();
        SendMail registerSendMail = new SendMail();
        registerSendMail.sendMailtoResetPwd(userEmail.Text.Trim(), userNameUserPwdUserEmail);
        Response.Write("<script language=javascript>alert('请到邮箱完成验证');location='Login.aspx';</script>");

    }
}