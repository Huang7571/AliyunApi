using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
	public class PermissionTypes//权限类型
	{
		/// <summary>
		/// 权限类型编号
		/// </summary>
		public int Pid                  { get; set; }
		/// <summary>
		/// 权限类型名字
		/// </summary>
		public string PermissionTypesName { get; set; }

	}
}