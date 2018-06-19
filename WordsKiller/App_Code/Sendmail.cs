using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services.Description;

/// <summary>
/// 发送邮箱服务,辅助 注册和重置密码
/// </summary>
public class SendMail
{
    /// <summary>
    /// 发送邮件类,几个private字段需要配置下才能让其利用hotmail正常发送邮件
    /// </summary>
    private const string from_email = "xiangshouxuan@hotmail.com";             //发送邮箱
    private const string from_email_password = "!!!需要更改为hotmail邮箱密码"; //发送方邮箱授权码or密码（qq邮箱授权码获取方式：QQ邮箱-设置-POP3/IMAP/SMTP/Exchange/CardDAV/CalDAV服务-生成授权码）
    private const string web_domain = "localhost:61787";                       //网站域名
    private const string email_signup_handle_web = "registerHandle.aspx";      //处理邮箱验证的网页
    private const string email_resetPwd_handle_web = "resetPwdHandle.aspx";    //处理邮箱验证的网页
    private const string web_name = "单词杀";                                  //网站名字 用于拼凑成邮件主题
    private const string subject = web_name + "网站注册验证码";                //发给用户的主题
    private const string smtp_ip = "smtp.office365.com";//"smtp.qq.com";       //SMTP的主机IP
    private const int smtp_ip_port = 587;//25;                                 //SMTP的主机端口号
    private const string html_body_partOneOfTwo = "<div id=\"mailContentContainer\" class=\"qmbox qm_con_body_content qqmail_webmail_only\">  <style type=\"text/css\">      .qmbox a:link {        color: #3466cc;      }          .qmbox a:visited {        color: #663399;      }      .qmbox a:active {        color: #cccccc;      }      .qmbox body, .qmbox td, .qmbox center, .qmbox p {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }      .qmbox .body {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }      .qmbox .disclaimer {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;        color:       #696969;      }      .qmbox .message {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;      }    .qmbox .qmbox a:link {        color: #3366cc;      }.qmbox .qmbox a:visited {        color: #663399;      }.qmbox .qmbox a:active {        color: #cccccc;      }.qmbox .qmbox body,.qmbox .qmbox td,.qmbox .qmbox center,.qmbox .qmbox p{        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }.qmbox .qmbox .body {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }.qmbox .qmbox .disclaimer {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;        color:       #696969;      }.qmbox .qmbox .message {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;      }</style>                    <br>    <br>    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\">      <tbody><tr>        <td align=\"left\" valign=\"top\">          <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"500\">            <tbody><tr>              <td align=\"left\" valign=\"top\"><img width=\"150\" height=\"44\" border=\"0\" src=\"http://r.mzstatic.com/email/images_shared/header_invoicereceipt_l.gif\"></td>              <td align=\"right\" valign=\"top\"><img width=\"328\" height=\"44\" border=\"0\" src=\"http://r.mzstatic.com/zh_cn/email/images_shared/header_bank_order_notice.gif\"></td>              <td align=\"right\" valign=\"top\">&nbsp;</td>            </tr>            <tr>              <td colspan=\"2\"><img width=\"500\" height=\"1\" border=\"0\" src=\"http://r.mzstatic.com/email/images_shared/spacer_999999.gif\"></td>            </tr>            <tr height=\"24\"><td colspan=\"2\" height=\"24\"></td></tr>            <tr>              <td colspan=\"2\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:10px;font-weight:normal;color:#333333;\">                  您好：                <p class=\"body\" style=\"font-size:10px;color:#333333;margin:1em 0;\">您的邮箱正在单词杀网进行注册，请点击<a href=\"";
    private const string html_body_partTwoOfTwo = "\">这里</a>完成注册。</p>                                <p class=\"body\" style=\"font-size:10px;color:#333333;margin:1em 0;\">如果您未进行此操作，请忽略本邮件。</p>                <p class=\"body salutation\" style=\"font-size:10px;color:#333333;margin:1em 0;\">                  此致                  <br>                    单词杀</p>              </td>            </tr>          </tbody></table>        </td>      </tr>      <tr height=\"75\"><td height=\"75\"></td></tr>      <tr>        <td>          <table align=\"center\">            <tbody><tr>              <td class=\"message\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#333333;\">                  &nbsp;</td>            </tr>            <tr>              <td class=\"message\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#333333;\">                  单词杀 尊重您的隐私。                <br>                  更多详情请访问                <a href=\"#\" target=\"_blank\">https://www.我的域名.com</a>                <br>                <br>              </td>            </tr>            <tr>              <td class=\"disclaimer\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#6e6e6e;\">                Copyright                © 2018 单词杀                <a href=\"#\" target=\"_blank\">                  保留所有权利                </a>              </td>            </tr>          </tbody></table>        </td>      </tr>    </tbody></table>    <br>    <br>  <img src=\"http://outsideapple.apple.com/img/APPLE_EMAIL_LINK/spacer4.gif?v=2&amp;a=KL%2BL%2BzoZnfLbBT1D3vsGxCxC7WXN7wjOgk0VJvX8pYpkyXJUnAIbeIpFUZKopOOY7RcLgCjKDPJihDefPnNhM12Q6YBnQgQmmZYgiPQAdPJGJZU7SuiLA%2FO14f2WrUyRm9NJZzVOr4w3uXZUSfSzFyKAhLpBXiWTSI%2Bp%2FLlo1HsDwH%2FeLiTjBEUbdLlL8JKlkdwzZvfuomz0c9xhpgGT7JXtFNmfjVoMnnixRBfBsiy14wGL5HF%2BGlicrX38KAhbNdp8x%2BtvyJjkHjmBvBJgv%2F63EI%2BpnlnkIpBz%2BDy3Izjs84jLUjf2ttcF9gMrpZSNdgojGi71daBl1eFzYb%2F8d4sFxy3MjpHNB9r40jS7NdzPPLXzA7uPiF0Rd3wanFuj\"><style type=\"text/css\">.qmbox style, .qmbox script, .qmbox head, .qmbox link, .qmbox meta {display: none !important;}</style></div>";

    /// <summary>
    /// 邮箱注册
    /// </summary>
    /// <param name="to_mail">接收方邮箱</param>
    /// <param name="userNameUserPwdUserEmail">用户名,用户哈希密码,用户邮箱三项组成的链接参数,如:userName=张三&userPassword=哈希密码&userEmail=123@qq.com</param>
    public void sendMailtoRegister(string to_mail, string userNameUserPwdUserEmail)
    {
        //设置邮件内容
        MailMessage myMessage = new MailMessage();
        myMessage.Subject = subject;

        string html_body_partOneOfTwo = "<div id=\"mailContentContainer\" class=\"qmbox qm_con_body_content qqmail_webmail_only\">  <style type=\"text/css\">      .qmbox a:link {        color: #3466cc;      }          .qmbox a:visited {        color: #663399;      }      .qmbox a:active {        color: #cccccc;      }      .qmbox body, .qmbox td, .qmbox center, .qmbox p {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }      .qmbox .body {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }      .qmbox .disclaimer {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;        color:       #696969;      }      .qmbox .message {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;      }    .qmbox .qmbox a:link {        color: #3366cc;      }.qmbox .qmbox a:visited {        color: #663399;      }.qmbox .qmbox a:active {        color: #cccccc;      }.qmbox .qmbox body,.qmbox .qmbox td,.qmbox .qmbox center,.qmbox .qmbox p{        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }.qmbox .qmbox .body {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }.qmbox .qmbox .disclaimer {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;        color:       #696969;      }.qmbox .qmbox .message {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;      }</style>                    <br>    <br>    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\">      <tbody><tr>        <td align=\"left\" valign=\"top\">          <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"500\">            <tbody><tr>              <td align=\"left\" valign=\"top\"><img width=\"150\" height=\"44\" border=\"0\" src=\"http://r.mzstatic.com/email/images_shared/header_invoicereceipt_l.gif\"></td>              <td align=\"right\" valign=\"top\"><img width=\"328\" height=\"44\" border=\"0\" src=\"http://r.mzstatic.com/zh_cn/email/images_shared/header_bank_order_notice.gif\"></td>              <td align=\"right\" valign=\"top\">&nbsp;</td>            </tr>            <tr>              <td colspan=\"2\"><img width=\"500\" height=\"1\" border=\"0\" src=\"http://r.mzstatic.com/email/images_shared/spacer_999999.gif\"></td>            </tr>            <tr height=\"24\"><td colspan=\"2\" height=\"24\"></td></tr>            <tr>              <td colspan=\"2\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:10px;font-weight:normal;color:#333333;\">                  您好：                <p class=\"body\" style=\"font-size:10px;color:#333333;margin:1em 0;\">您的邮箱正在单词杀网进行注册，请点击<a href=\"";
        string html_body_partTwoOfTwo = "\">这里</a>完成注册。</p>                                <p class=\"body\" style=\"font-size:10px;color:#333333;margin:1em 0;\">如果您未进行此操作，请忽略本邮件。</p>                <p class=\"body salutation\" style=\"font-size:10px;color:#333333;margin:1em 0;\">                  此致                  <br>                    单词杀</p>              </td>            </tr>          </tbody></table>        </td>      </tr>      <tr height=\"75\"><td height=\"75\"></td></tr>      <tr>        <td>          <table align=\"center\">            <tbody><tr>              <td class=\"message\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#333333;\">                  &nbsp;</td>            </tr>            <tr>              <td class=\"message\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#333333;\">                  单词杀 尊重您的隐私。                <br>                  更多详情请访问                <a href=\"#\" target=\"_blank\">https://www.我的域名.com</a>                <br>                <br>              </td>            </tr>            <tr>              <td class=\"disclaimer\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#6e6e6e;\">                Copyright                © 2018 单词杀                <a href=\"#\" target=\"_blank\">                  保留所有权利                </a>              </td>            </tr>          </tbody></table>        </td>      </tr>    </tbody></table>    <br>    <br>  <img src=\"http://outsideapple.apple.com/img/APPLE_EMAIL_LINK/spacer4.gif?v=2&amp;a=KL%2BL%2BzoZnfLbBT1D3vsGxCxC7WXN7wjOgk0VJvX8pYpkyXJUnAIbeIpFUZKopOOY7RcLgCjKDPJihDefPnNhM12Q6YBnQgQmmZYgiPQAdPJGJZU7SuiLA%2FO14f2WrUyRm9NJZzVOr4w3uXZUSfSzFyKAhLpBXiWTSI%2Bp%2FLlo1HsDwH%2FeLiTjBEUbdLlL8JKlkdwzZvfuomz0c9xhpgGT7JXtFNmfjVoMnnixRBfBsiy14wGL5HF%2BGlicrX38KAhbNdp8x%2BtvyJjkHjmBvBJgv%2F63EI%2BpnlnkIpBz%2BDy3Izjs84jLUjf2ttcF9gMrpZSNdgojGi71daBl1eFzYb%2F8d4sFxy3MjpHNB9r40jS7NdzPPLXzA7uPiF0Rd3wanFuj\"><style type=\"text/css\">.qmbox style, .qmbox script, .qmbox head, .qmbox link, .qmbox meta {display: none !important;}</style></div>";
        myMessage.Body = html_body_partOneOfTwo + web_domain + "/" + email_signup_handle_web + "?" + userNameUserPwdUserEmail + html_body_partTwoOfTwo;

        myMessage.From = new MailAddress(from_email, "无需回复此邮件,请点击正文链接完成注册");
        myMessage.To.Add(new MailAddress(to_mail));
        myMessage.IsBodyHtml = true;

        //设置邮件服务配置
        SmtpClient mySmtpClient = new SmtpClient
        {
            Host = smtp_ip,
            Port = smtp_ip_port,
            Credentials = new System.Net.NetworkCredential(from_email, from_email_password),
            EnableSsl = true
        };

        //指定 SmtpClient 使用安全套接字层(SSL)加密连接

        //一切配置完成，发送邮件
        try
        {
            mySmtpClient.Send(myMessage);
            System.Diagnostics.Debug.WriteLine("发送邮箱成功！");
        }
        catch (SmtpException ex)
        {
            throw;
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// 通过邮箱重置密码
    /// </summary>
    /// <param name="to_mail">已注册的邮箱</param>
    /// <param name="userNameUserPwdUserEmail">用户名,用户哈希密码,用户邮箱三项组成的链接参数,如:userName=张三&userPassword=哈希密码&userEmail=123@qq.com</param>
    public void sendMailtoResetPwd(string to_mail, string userNameUserPwdUserEmail)
    {
        //设置邮件内容
        MailMessage myMessage = new MailMessage();
        myMessage.Subject = subject;

        string html_body_partOneOfTwo = "<div id=\"mailContentContainer\" class=\"qmbox qm_con_body_content qqmail_webmail_only\">  <style type=\"text/css\">      .qmbox a:link {        color: #3466cc;      }          .qmbox a:visited {        color: #663399;      }      .qmbox a:active {        color: #cccccc;      }      .qmbox body, .qmbox td, .qmbox center, .qmbox p {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }      .qmbox .body {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }      .qmbox .disclaimer {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;        color:       #696969;      }      .qmbox .message {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;      }    .qmbox .qmbox a:link {        color: #3366cc;      }.qmbox .qmbox a:visited {        color: #663399;      }.qmbox .qmbox a:active {        color: #cccccc;      }.qmbox .qmbox body,.qmbox .qmbox td,.qmbox .qmbox center,.qmbox .qmbox p{        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }.qmbox .qmbox .body {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   10px;        color:       #333333;      }.qmbox .qmbox .disclaimer {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;        color:       #696969;      }.qmbox .qmbox .message {        font-family: Geneva, Verdana, Arial, Helvetica;        font-size:   9px;      }</style>                    <br>    <br>    <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"center\">      <tbody><tr>        <td align=\"left\" valign=\"top\">          <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"500\">            <tbody><tr>              <td align=\"left\" valign=\"top\"><img width=\"150\" height=\"44\" border=\"0\" src=\"http://r.mzstatic.com/email/images_shared/header_invoicereceipt_l.gif\"></td>              <td align=\"right\" valign=\"top\"><img width=\"328\" height=\"44\" border=\"0\" src=\"http://r.mzstatic.com/zh_cn/email/images_shared/header_bank_order_notice.gif\"></td>              <td align=\"right\" valign=\"top\">&nbsp;</td>            </tr>            <tr>              <td colspan=\"2\"><img width=\"500\" height=\"1\" border=\"0\" src=\"http://r.mzstatic.com/email/images_shared/spacer_999999.gif\"></td>            </tr>            <tr height=\"24\"><td colspan=\"2\" height=\"24\"></td></tr>            <tr>              <td colspan=\"2\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:10px;font-weight:normal;color:#333333;\">                  您好：                <p class=\"body\" style=\"font-size:10px;color:#333333;margin:1em 0;\">您的单词杀账户正在更改密码，确认请点击<a href=\"";
        string html_body_partTwoOfTwo = "\">这里</a>完成更改。</p>                                <p class=\"body\" style=\"font-size:10px;color:#333333;margin:1em 0;\">如果您未进行此操作，请忽略本邮件。</p>                <p class=\"body salutation\" style=\"font-size:10px;color:#333333;margin:1em 0;\">                  此致                  <br>                    单词杀</p>              </td>            </tr>          </tbody></table>        </td>      </tr>      <tr height=\"75\"><td height=\"75\"></td></tr>      <tr>        <td>          <table align=\"center\">            <tbody><tr>              <td class=\"message\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#333333;\">                  &nbsp;</td>            </tr>            <tr>              <td class=\"message\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#333333;\">                  单词杀 尊重您的隐私。                <br>                  更多详情请访问                <a href=\"#\" target=\"_blank\">https://www.我的域名.com</a>                <br>                <br>              </td>            </tr>            <tr>              <td class=\"disclaimer\" align=\"center\" width=\"500\" style=\"font-family:Geneva,Verdana,Arial,Helvetica;font-size:9px;color:#6e6e6e;\">                Copyright                © 2018 单词杀                <a href=\"#\" target=\"_blank\">                  保留所有权利                </a>              </td>            </tr>          </tbody></table>        </td>      </tr>    </tbody></table>    <br>    <br>  <img src=\"http://outsideapple.apple.com/img/APPLE_EMAIL_LINK/spacer4.gif?v=2&amp;a=KL%2BL%2BzoZnfLbBT1D3vsGxCxC7WXN7wjOgk0VJvX8pYpkyXJUnAIbeIpFUZKopOOY7RcLgCjKDPJihDefPnNhM12Q6YBnQgQmmZYgiPQAdPJGJZU7SuiLA%2FO14f2WrUyRm9NJZzVOr4w3uXZUSfSzFyKAhLpBXiWTSI%2Bp%2FLlo1HsDwH%2FeLiTjBEUbdLlL8JKlkdwzZvfuomz0c9xhpgGT7JXtFNmfjVoMnnixRBfBsiy14wGL5HF%2BGlicrX38KAhbNdp8x%2BtvyJjkHjmBvBJgv%2F63EI%2BpnlnkIpBz%2BDy3Izjs84jLUjf2ttcF9gMrpZSNdgojGi71daBl1eFzYb%2F8d4sFxy3MjpHNB9r40jS7NdzPPLXzA7uPiF0Rd3wanFuj\"><style type=\"text/css\">.qmbox style, .qmbox script, .qmbox head, .qmbox link, .qmbox meta {display: none !important;}</style></div>";
        myMessage.Body = html_body_partOneOfTwo + web_domain + "/" + email_resetPwd_handle_web + "?" + userNameUserPwdUserEmail + html_body_partTwoOfTwo;

        myMessage.From = new MailAddress(from_email, "无需回复此邮件,请点击正文链接重置密码");
        myMessage.To.Add(new MailAddress(to_mail));
        myMessage.IsBodyHtml = true;

        //设置邮件服务配置
        SmtpClient mySmtpClient = new SmtpClient
        {
            Host = smtp_ip,
            Port = smtp_ip_port,
            Credentials = new System.Net.NetworkCredential(from_email, from_email_password),
            EnableSsl = true
        };

        //指定 SmtpClient 使用安全套接字层(SSL)加密连接

        //一切配置完成，发送邮件
        try
        {
            mySmtpClient.Send(myMessage);
            System.Diagnostics.Debug.WriteLine("发送邮箱成功！");
        }
        catch (SmtpException ex)
        {
            throw;
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }
}

