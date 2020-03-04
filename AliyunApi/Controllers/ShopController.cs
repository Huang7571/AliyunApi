using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using AliyunApi.Models;

namespace AliyunApi.Controllers
{
    /// <summary>
    /// 商品
    /// </summary>
    public class ShopController : ApiController
    {
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
            string sql = "select * from BigType";
            return DBHelper.GetToList<BigType>(sql);
        }
        /// <summary>
        /// 小类表展示
        /// </summary>
        /// <returns></returns>
        public List<SmallType> GetSmallType()
        {
            string sql = "select * from SmallType";
            return DBHelper.GetToList<SmallType>(sql);
        }
        /// <summary>
        /// 商品展示
        /// </summary>
        /// <returns></returns>
        public List<Shop> GetShop()
        {
            string sql = "select * from Shop join BigType on Shop.Btid=BigType.Btid join SmallType on Shop.Stid=SmallType.Stid join ShopSize on Shop.Ssid=ShopSize.Ssid";
            return DBHelper.GetToList<Shop>(sql);
        }
        /// <summary>
        /// 批量删除商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteShop(string id)
        {
            string[] ids = new string[id.Length];
            string sql = "";
            foreach (string item in ids)
            {
                int a = Convert.ToInt32(item);
                sql = $"delete from Shop where Sid={a}";
            }
            return DBHelper.ExecuteNonQuery(sql);

        }
        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateShop(Shop model)
        {
            string sql = $"update Shop set Stype='{model.Stype}',Sname='{model.Sname}',Stitle='{model.Stitle}',Simg='{model.Simg}',Sprice='{model.Sprice}',Ssize='{model.Ssize}',Sscore='{model.Sscore}',Sdescribe='{model.Sdescribe}',Safter='{model.Safter}',Stime='{model.Stime}',Sstate='{model.Sstate}',Saudit='{model.Saudit}',Btid='{model.Btid}',Stid='{model.Stid}',Sstock='{model.Sstock}',Scolor='{model.Scolor}',Ssnum='{model.Ssnum}'";
            return DBHelper.ExecuteNonQuery(sql);
        }
    }
    /// <summary>
    /// 帮助类
    /// </summary>
    public class DBHelper
    {
        //连接数据库
        static SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Shop;Integrated Security=True");
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
}
