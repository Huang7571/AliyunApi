using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using AliyunApi.Models;
using Newtonsoft.Json;
using System.Configuration;

namespace AliyunApi.Controllers
{
    /// <summary>
    /// 商品
    /// </summary>
    public class ShopController : ApiController
    {
        string Mysql = ConfigurationManager.ConnectionStrings["Sqlconn"].ToString();
        /// <summary>
        /// 大类表类型添加
        /// </summary>
        /// <returns></returns>
        public int PostBigType(BigType model)
        {
            string sql = $"insert into BigType values('{model.Btname}')";
            return DBHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 小类表类型添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int PostSmallType(SmallType model)
        {
            string sql = $"insert into SmallType values('{model.Stname}')";
            return DBHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 商品表添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int PostShop(Shop model)
        {
            string sql = $"insert into Shop values('{model.Stype}','{model.Sname}','{model.Stitle}','{model.Simg}','{model.Scolor}','{model.Ssize}','{model.Ssnum}',{model.Sstock},{model.Sprice},{model.Sscore},'{model.Sdescribe}','{model.Safter}','{0}','{model.Sstate}',{0},{model.Btid},{model.Stid},'{DateTime.Now}',{model.MerchantId});";
            return DBHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 大类表展示
        /// </summary>
        /// <returns></returns>
        public List<BigType> GetBigType()
        {
            using (SqlConnection coon = new SqlConnection(Mysql))
            {
                SqlCommand cmd = coon.CreateCommand();
                string sql = "select * from BigType";
                cmd.CommandText = sql;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("dt");
                sda.Fill(dt);
                return JsonConvert.DeserializeObject<List<BigType>>(JsonConvert.SerializeObject(dt));
            }
        }
        /// <summary>
        /// 小类表展示
        /// </summary>
        /// <returns></returns>
        public List<SmallType> GetSmallType()
        {
            using (SqlConnection coon = new SqlConnection(Mysql))
            {
                SqlCommand cmd = coon.CreateCommand();
                string sql = "select * from SmallType";
                cmd.CommandText = sql;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("dt");
                sda.Fill(dt);
                return JsonConvert.DeserializeObject<List<SmallType>>(JsonConvert.SerializeObject(dt));
            }

        }
        /// <summary>
        /// 商品展示
        /// </summary>
        /// <returns></returns>
        public ReturnModel PostShopShow(Pager pager)
        {
            using (SqlConnection coon = new SqlConnection(Mysql))
            {
                SqlCommand cmd = coon.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Shop_pager";
                SqlParameter[] sp = new SqlParameter[]
                {
                   new SqlParameter { ParameterName="Sname",SqlDbType=SqlDbType.VarChar,SqlValue=pager.Sname},
                   new SqlParameter{ ParameterName="pageIndex",SqlDbType=SqlDbType.Int,SqlValue=pager.pageIndex },
                   new SqlParameter{ ParameterName="pageSize",SqlDbType=SqlDbType.Int,SqlValue=pager.pageSize },
                   new SqlParameter{ ParameterName="totalCount",SqlDbType=SqlDbType.Int,Direction=ParameterDirection.Output }
                };
                cmd.Parameters.AddRange(sp);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Mydt");
                sda.Fill(dt);
                ReturnModel rm = new ReturnModel()
                {
                    list = JsonConvert.DeserializeObject<List<Shop>>(JsonConvert.SerializeObject(dt)),
                    TotalCount = Convert.ToInt32(cmd.Parameters["totalCount"].Value)
                };
                return rm;
            }
        }
        public List<Shop> GetFoundShop(int id)
        {
            string sql = $"select * from shop where Sid={id}";
            return DBHelper.GetToList<Shop>(sql);
        }
        /// <summary>
        /// 批量删除商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetDeleteShop(int id)
        {
            string sql = $"delete from Shop where Sid={id}";
            return DBHelper.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateShop(Shop model)
        {
            string sql = $"update Shop set Stype='{model.Stype}',Sname='{model.Sname}',Stitle='{model.Stitle}',Simg='{model.Simg}',Sprice='{model.Sprice}',Ssize='{model.Ssize}',Sscore='{model.Sscore}',Sdescribe='{model.Sdescribe}',Safter='{model.Safter}',Stime='{model.Stime}',Sstate='{model.Sstate}',Btid='{model.Btid}',Stid='{model.Stid}',Sstock='{model.Sstock}',Scolor='{model.Scolor}',Ssnum='{model.Ssnum}' where Sid={model.Sid}";
            return DBHelper.ExecuteNonQuery(sql);
        }
        TimeSpan tspan = DateTime.Now - new DateTime(1970, 1, 1);

        /// <summary>
        /// 图片上传接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string FileUpload()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];//获取http传输的文件
                string domainPath = HttpRuntime.AppDomainAppPath.ToString();//物理路径 二选一
                string serverPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Image/");//物理路径
                string ext = Path.GetExtension(file.FileName);//获取扩展名
                string newName = tspan.TotalMilliseconds + ext;//时间戳+扩展名形成新文件名

                file.SaveAs(@"C:\Users\Lenovo\Desktop\AliyunMvc\AliyunMvc\Image\" + tspan.TotalMilliseconds + ext);
                return @"C:\Users\Lenovo\Desktop\AliyunMvc\AliyunMvc\Image\" + tspan.TotalMilliseconds + ext;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
/// <summary>
/// 帮助类
/// </summary>
public class DBHelper
{
    static string Mysql = ConfigurationManager.ConnectionStrings["Sqlconn"].ToString();
    //连接数据库
    static SqlConnection conn = new SqlConnection(Mysql);
    static SqlDataReader sdr;
    /// <summary>
    /// 获取数据流  查询、显示、绑定下拉
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    private static SqlDataReader GetDataReader(string sql)
    {
        try
        {
            //打开
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //命令对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            sdr = cmd.ExecuteReader();
            return sdr;
        }
        catch (Exception)
        {
            if (sdr != null && !sdr.IsClosed)//数据流关闭
            {
                sdr.Close();
            }
            throw;
        }

    }
    /// <summary>
    /// 返回受影响行数  
    /// 添加、删除、修改
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static int ExecuteNonQuery(string sql)
    {
        try
        {
            //打开
            //判断状态
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            //命令对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            int n = cmd.ExecuteNonQuery();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return n;
        }
        catch (Exception)
        {

            throw;
        }
    }
    /// <summary>
    /// 数据流转List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sdr"></param>
    /// <returns></returns>
    private static List<T> DataReaderToList<T>(SqlDataReader sdr)
    {
        Type t = typeof(T);//获取类型
                           //获取所有属性
        PropertyInfo[] p = t.GetProperties();
        //定义集合
        List<T> list = new List<T>();
        //遍历数据流
        while (sdr.Read())
        {
            //创建对象
            T obj = (T)Activator.CreateInstance(t);
            //数据流列数
            string[] sdrFileName = new string[sdr.FieldCount];
            for (int i = 0; i < sdr.FieldCount; i++)
            {
                sdrFileName[i] = sdr.GetName(i).Trim();
            }
            foreach (PropertyInfo item in p)
            {
                //判断Model中的属性是否在流的列名中
                if (sdrFileName.ToList().IndexOf(item.Name) > -1)
                {
                    if (sdr[item.Name] != null)
                    {
                        item.SetValue(obj, sdr[item.Name]);//对象属性赋值
                    }
                    else
                    {
                        item.SetValue(obj, null);//对象属性赋值
                    }
                }
                else
                {
                    item.SetValue(obj, null);//对象属性赋值
                }

            }
            list.Add(obj);
        }
        return list;
    }
    /// <summary>
    /// 获取list集合
    ///  查询、显示、绑定下拉
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetToList<T>(string sql)
    {
        //获取流数据
        SqlDataReader sdr = GetDataReader(sql);
        List<T> list = DataReaderToList<T>(sdr);
        if (!sdr.IsClosed)//数据流关闭
        {
            sdr.Close();
        }
        return list;
    }
    /// <summary>
    /// 返回首行首列
    /// 登录
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static object ExecuteScalar(string sql)
    {
        try
        {
            //打开
            //判断状态
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            //命令对象
            SqlCommand cmd = new SqlCommand(sql, conn);
            object n = cmd.ExecuteScalar();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return n;
        }
        catch (Exception)
        {

            throw;
        }
    }

}
