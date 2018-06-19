using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// 数据库操作类
/// </summary>
public class SqlData
{
    private SqlConnection sqlcon;       //申明一个SqlConnection对象
    private SqlCommand sqlcom;          //申明一个SqlCommand对象
    private SqlDataAdapter sqldata;     //申明一个SqlDataAdapter对象

    /// <summary>
    ///构造函数用于连接数据库,每次建立对象都先把数据库连接上
    /// </summary>
    public SqlData()
    {
        sqlcon = new SqlConnection("Data Source=localhost;Initial Catalog=WordsKiller;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        sqlcon.Open();
    }


    /// <summary>
    ///  执行 不返回数据集/操作成功条数 的SQL语句
    /// </summary>
    /// <param name="strSQL">SQL字符串</param>
    /// <returns>成功操作返回true</returns>
    public bool ExceSQL(string strSQL)
    {
        sqlcom = new SqlCommand(strSQL, sqlcon);
        try
        {
            sqlcom.ExecuteNonQuery();
            return true;
        }
        catch
        {
            throw;
        }
        finally
        {
            sqlcon.Close();
        }
    }
    
    /// <summary>
    /// 执行 返回DataSet 的SQl语句
    /// </summary>
    /// <param name="strSQL">SQL字符串</param>
    /// <returns>查询成功得到的DataSet</returns>
    public DataSet ExceDS(string strSQL)
    {
        try
        {
            sqlcom = new SqlCommand(strSQL, sqlcon);
            sqldata = new SqlDataAdapter();
            sqldata.SelectCommand = sqlcom;
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            return ds;
        }
        finally
        {
            sqlcon.Close();
        }
    }

    /// <summary>
    /// 执行 返回SqlDataReader 的SQL语句
    /// </summary>
    /// <param name="strSQL">SQL字符串</param>
    /// <returns>查询成功得到的DataReader</returns>
    public SqlDataReader ExceRead(string strSQL)
    {
        try
        {
            sqlcom = new SqlCommand(strSQL, sqlcon);
            SqlDataReader read = sqlcom.ExecuteReader();
            return read;
        }
        finally
        {
            //sqlcon.Close();
        }

    }
}