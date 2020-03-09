using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
	public class Administrator//管理员表
	{
		/// <summary>
		/// 管理员编号ss
		/// </summary>
		public int Aid                  { get; set; }
		/// <summary>
		/// 管理员名字
		/// </summary>
		public string AdministratorName    { get; set; }
		/// <summary>
		/// 是否启用
		/// </summary>
		public int Menable              { get; set; }

	}
}