using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using AliyunApi.Models;
using Dapper;
using System.Configuration;
namespace AliyunApi.Controllers
{
    public class OrderForm2Controller : ApiController
    {
		string Mysql = ConfigurationManager.ConnectionStrings["Sqlconn"].ToString();
		/// <summary>
		/// 显示条数据信息
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public pShowModel Getbufen(int id = 7)
        {
            List<orderform> orderformlist = new List<orderform>();
			using (SqlConnection conn = new SqlConnection(Mysql))
			{
				orderformlist = conn.Query<orderform>($"select od.*,sh.Sname,sh.Stitle,sh.Simg,sh.Sprice,sh.Scolor,sh.Ssnum,sh.Sdescribe,(case od.OF_status when 1 then '待处理' when 2 then '揽货中' when 3 then '已发货' when 4 then '已完成' when 5 then '取消订单' when 6 then '退货' end) as status from orderform od join Shop sh on od.Shop_Sid = sh.Sid where od.OF_Id={id}").ToList();
			}
            pShowModel model = new pShowModel();
            model.code = 200;
            model.msg = "msg";
            model.count = orderformlist.Count();
            model.data = orderformlist;
            return model;
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public int Post(orderform model)
        {
            using (SqlConnection conn = new SqlConnection(Mysql))
            {
                return conn.Execute($"update orderform set orderform.OF_status='{model.OF_status}' where OF_Id={model.OF_Id}");
            }
        }

    }
}
