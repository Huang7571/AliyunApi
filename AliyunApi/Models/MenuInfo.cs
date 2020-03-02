using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
	public class MenuInfo//权限表
	{
		/// <summary>
		/// 权限编号
		/// </summary>
		public int Mid { get; set; }
		/// <summary>
		/// 权限名字
		/// </summary>
		public string  MenuName { get; set; }
		/// <summary>
		/// 权限类型
		/// </summary>
		public int PermissionTypesId { get; set; }
		/// <summary>
		/// 是否启用
		/// </summary>
		public int Menable { get; set; }
	}
}