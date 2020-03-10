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
    public class OrderForm3Controller : ApiController
    {
        /// <summary>
        /// 绑定下拉
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<logistics> Getlist()
        {
            List<logistics> orderformlist = new List<logistics>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Sqlconn"].ToString()))
			{
				orderformlist = conn.Query<logistics>($"select * from logistics").ToList();
			}
            return orderformlist;
        }
    }
}
