using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AliyunApi.Models;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;
namespace AliyunApi.Controllers
{
	public class PermissionController : ApiController
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sqlconn"].ToString());
		/// <summary>
		/// 显示权限晋力
		/// </summary>
		/// <param name="AdministratorName"></param>
		/// <returns></returns>
		public DataTable GetAdministrator(string MerchantName)
		{
			string Yonghu = @"select * ,(case MerchantState when 1 then '启用' else '禁用' end) as MState
 from Merchant a join Administrator b on a.Aid = b.Aid where MerchantName ='" + MerchantName + "'";//显示用户信息
			SqlDataAdapter sqlData = new SqlDataAdapter(Yonghu, con);
			DataTable dataTable = new DataTable("Datatable");
			sqlData.Fill(dataTable);
			List<Merchant> list = JsonConvert.DeserializeObject<List<Merchant>>(JsonConvert.SerializeObject(dataTable));
			Merchant merchant = list.FirstOrDefault();
			string sql = $@"select a.AdministratorName,stuff((select ','+MenuName from view_Menu 
where a.AdministratorName = AdministratorName for xml path('')),1,1,'') as MenuName
from view_Menu as a group by AdministratorName having a.AdministratorName = '{merchant.AdministratorName}'";//显示用户对应的权限
			SqlDataAdapter ap = new SqlDataAdapter(sql, con);
			DataTable table = new DataTable("Mytable");
			ap.Fill(table);

			return table;
		}
		/// <summary>
		/// 修改权限
		/// </summary>
		/// <param name="Permission"></param>
		/// <param name="Aid"></param>
		/// <returns></returns>
		public int GetPer(string Permission, int Aid)
		{
			con.Open();
			string[] Ps = Permission.Split(',');//分割字符串
			SqlCommand cmd = con.CreateCommand();
			cmd.CommandText = "delete from Administratorsgroup where Aid=" + Aid;//删除原有的全部权限
			int n = cmd.ExecuteNonQuery();
			int c=0;
			if (n > 0)
			{
				foreach (string item in Ps)//遍历
				{
					cmd.CommandText = "select Mid from MenuInfo where MenuName='" + item + "'";//获得查询到的权限编号
					int b = Convert.ToInt32(cmd.ExecuteScalar());
					cmd.CommandText = $"insert into Administratorsgroup values('{Aid}',{b})";//添加
					c += cmd.ExecuteNonQuery();
				}
				return c;	
			}
			else
			{
				con.Close();
				return n;
			}

		}
		/// <summary>
		/// 管理员下拉
		/// </summary>
		/// <returns></returns>
		public DataTable GetAdmin()
		{
			SqlDataAdapter ap = new SqlDataAdapter("select * from Administrator", con);
			DataTable table = new DataTable("Mytable");
			ap.Fill(table);
			return table;
		}

	}

}
