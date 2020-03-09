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
namespace AliyunApi.Controllers
{
	public class PermissionController : ApiController
	{
		SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ShopingOA;Integrated Security=True");
		/// <summary>
		/// 显示权限
		/// </summary>
		/// <param name="AdministratorName"></param>
		/// <returns></returns>
		public DataTable GetAdministrator(string AdministratorName)
		{
			string sql = $"select * from Administratorsgroup a join Administrator b on a.Aid=b.Aid join MenuInfo c on a.Mid=c.Mid where b.AdministratorName='{AdministratorName}'";//显示用户对应的权限
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
		public int PutAdministrator(string Permission, int Aid)
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


	}

}
