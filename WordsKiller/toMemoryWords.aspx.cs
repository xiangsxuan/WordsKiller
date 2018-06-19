using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class About : Page
{
    public string word="[单词]";
    public string explanation="[释义]";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Write("<script language=javascript>alert('尚未登陆,请登陆后学习');location='Login.aspx'</script>");
        }
        else
        {
            SqlData da = new SqlData();
            String sqlNextWord = "select * from words where wordId=(select nextWordId from people where userName='" +
                                 Session["userName"] + "')";
            SqlDataReader sqlDataReader = da.ExceRead(sqlNextWord);
            //Response.Write("1<br>");
            if (sqlDataReader.HasRows)
            {
                //Response.Write("2<br>");
                // Response.Write(sqlDataReader.ToString());
                sqlDataReader.Read();
                word = sqlDataReader.GetValue(0).ToString().Trim();
                explanation = sqlDataReader.GetValue(1).ToString().Trim();
            }
            else
            {
                //Response.Write("5<br>");

                Response.Write(
                    "<script language=javascript>alert('请联系管理员,您的单词库出现错误！');/*location='Register.aspx'*/;</script>");
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Write("<script language=javascript>alert('登陆状态失效！请登陆');location='Login.aspx'</script>");
        }
        else
        {
            SqlData daWordUpdate = new SqlData();
            string sqlString = "Update people set nextWordId=nextWordId+1 where userName='" + Session["userName"] + "'";
            //Response.Write("1111");
            bool isNextWordUpdated = daWordUpdate.ExceSQL(sqlString);
            //Response.Write("2222");
            if (isNextWordUpdated)
            {
                SqlData daLearnRec = new SqlData();
                // Response.Write(Request.Form["xingbie"].ToString());
                string sqlLearningRecord = "Insert into LearningRecord (userName,learnTime) Values('" + Session["userName"] + "','" + DateTime.Now + "')";
                daLearnRec.ExceSQL(sqlLearningRecord);
                Response.Redirect("toMemoryWords.aspx");
            }
            else
            {
                Response.Write("<script language=javascript>alert('记录失败！请联系管理员');</script>");
            }
        }
    }
}