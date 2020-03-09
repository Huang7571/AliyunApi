using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliyunApi.Models
{
	public class Administratorsgroup//管理员组
	{
		/// <summary>
		/// 主键
		/// </summary>
		public int Gid { get; set; }
		/// <summary>
		/// 管理员编号
		/// </summary>
		public int Aid { get; set; }
		/// <summary>
		/// 菜单编号
		/// </summary>
		public int Mid { get; set; }
	}
}