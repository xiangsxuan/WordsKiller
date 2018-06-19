using System;
using System.Web.UI;
public partial class AccountRegister : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {

        String userNameUserPwdUserEmail = "userName=" + UserName.Text.Trim() +
                                         "&userPwd=" + Password.Text.Trim().GetHashCode() +
                                         "&userEmail=" + userEmail.Text.Trim();
        SendMail registerSendMail = new SendMail();
        registerSendMail.sendMailtoRegister(userEmail.Text.Trim(), userNameUserPwdUserEmail);
        Response.Write("<script language=javascript>alert('请到邮箱完成验证');location='Login.aspx';</script>");

        //项目初始化带的membership相关,不用了
        //var manager = new UserManager();
        //var user = new ApplicationUser() { UserName = UserName.Text };
        //IdentityResult result = manager.Create(user, Password.Text);
        //if (result.Succeeded)
        //{
        //    IdentityHelper.SignIn(manager, user, isPersistent: false);
        //    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        //}
        //else
        //{
        //ErrorMessage.Text = result.Errors.FirstOrDefault();
        //}
    }
}