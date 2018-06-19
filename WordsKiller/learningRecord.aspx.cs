using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class About : Page
{
    
    public string strWordsNum = "[";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            Response.Write("<script language=javascript>alert('登陆失效,请登陆');location='Login.aspx';</script>");

        }
        else
        {
            SqlData da = new SqlData();
            //这是返回30天的数据的SQL语句 string sqlString = "select * from learningRecord where DATEDIFF(day,GETDATE(),learnTime) < 30 and userName='"+Session["userName"]+"' ORDER BY learnTime aSC";
            string sqlString = "select COUNT(*) as 次数,day(learnTime) as 日 from learningRecord where DATEDIFF(day, GETDATE(), learnTime) < 7 and userName = '" + Session["userName"] + "'group by day(learnTime)";
            SqlDataReader sqlDataReader = da.ExceRead(sqlString);
            if (sqlDataReader.HasRows)
            {
                int i = Convert.ToInt32((DateTime.Now.Day.ToString()))-7;
               // Response.Write("第 前7天的号:");
               // Response.Write(Convert.ToInt32((DateTime.Now.Day.ToString())) - 7);
               // Response.Write("<br>");

                while (sqlDataReader.Read())
                {
                    //Response.Write("<font >"+sqlDataReader.GetValue(0).ToString()+"</>");
                    
                    if (i==Convert.ToInt16(sqlDataReader.GetValue(1).ToString()))
                    {
                        strWordsNum = strWordsNum+sqlDataReader.GetValue(0)+",";
                        i++;
                    }
                    //wordNum[i] = Convert.ToInt16(sqlDataReader.GetValue(0).ToString());

                }
                strWordsNum = strWordsNum.TrimEnd(',') + "]";
                
            }
            else
            {
                Response.Write("暂无学习记录<br>");
            }

           
        }
    }
}