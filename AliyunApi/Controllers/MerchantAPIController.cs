using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using AliyunApi.Models;
using Newtonsoft.Json;
using Dapper;
using System.Data;
using System.Configuration;
namespace AliyunApi.Controllers
{
	public class MerchantAPIController : ApiController
	{
		SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Sqlconn"].ToString());
		/// <summary>
		/// 登录
		/// </summary>
		/// <param name="json"></param>
		/// <returns></returns>
		public int GetLogin(string json)
		{
			Merchant model = JsonConvert.DeserializeObject<Merchant>(json);
			conn.Open();
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"select count(1) from Merchant where MerchantName='{model.MerchantName}' and MerchantPwd='{model.MerchantPwd}'";
			int n = Convert.ToInt32(cmd.ExecuteScalar());
			conn.Close();
			return n;
		}
		/// <summary>
		/// 注册(添加用户)
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int PostMerchant(Merchant model)
		{
			conn.Open();
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"insert into Merchant values('{model.MerchantName}','{model.MerchantEmail}','{model.MerchantPwd}','{model.MerchantContent}','{model.Aid}','{model.MerchantState}')";
			int n = cmd.ExecuteNonQuery();
			conn.Close();
			return n;
		}
		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int PutMerchant(Merchant model)
		{
			conn.Open();
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"update Merchant set MerchantName='{model.MerchantName}',MerchantEmail='{model.MerchantEmail}',MerchantPwd='{model.MerchantPwd}',MerchantContent='{model.MerchantContent}',Aid='{model.Aid}',MerchantState='{model.MerchantState}' where MerchantId='{model.MerchantId}'";

			int n = cmd.ExecuteNonQuery();
			conn.Close();
			return n;
		}
		public PageModel GetPage(int PageIndex = 1, int PageSize = 3, int MerchantId = 0)//显示分页
		{
			using (IDbConnection conn = new SqlConnection("Data Source=.;Initial Catalog=ShopingOA;Integrated Security=True"))
			{
				if (MerchantId == 0)//等于0就是分页
				{
					List<Merchant> list = conn.Query<Merchant>(@"select * ,(case MerchantState when 1 then '启用' else '禁用' end) as MState
 from Merchant a join Administrator b on a.Aid = b.Aid").ToList();
					PageModel model = new PageModel();
					model.code = 0;
					model.msg = "msg";
					model.count = list.Count();
					model.data = list.Skip(PageSize * (PageIndex - 1)).Take(PageSize).ToList();
					return model;
				}
				else//反之查询要修改的编号
				{
					List<Merchant> list = conn.Query<Merchant>(@"select * ,(case MerchantState when 1 then '启用' else '禁用' end) as MState
 from Merchant a join Administrator b on a.Aid = b.Aid where MerchantId=" + MerchantId + "").ToList();
					PageModel model = new PageModel();
					model.code = 0;
					model.msg = "msg";
					model.count = list.Count();
					model.data = list;
					return model;
				}

			}
		}
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public int DeleteMerchant(int id)
		{
			conn.Open();
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"delete from Merchant where MerchantId='{id}'";
			int n = cmd.ExecuteNonQuery();
			conn.Close();
			return n;
		}
		
	}
}
