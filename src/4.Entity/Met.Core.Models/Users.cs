using System;

namespace Met.Core.Models
{	
	/// <summary>
	/// Users
	/// </summary>	
	public partial class Users
	{
	    /// <summary>
	    /// 
	    /// </summary>	
	    public int Id { get; set; }
	    /// <summary>
	    /// 用户名
	    /// </summary>	
	    public string UserName { get; set; }
	    /// <summary>
	    /// 真实姓名
	    /// </summary>	
	    public string TrueName { get; set; }
	    /// <summary>
	    /// 密码
	    /// </summary>	
	    public string Password { get; set; }
	    /// <summary>
	    /// 邮箱
	    /// </summary>	
	    public string Email { get; set; }
	    /// <summary>
	    /// 电话
	    /// </summary>	
	    public string Phone { get; set; }
	    /// <summary>
	    /// 地址
	    /// </summary>	
	    public string Address { get; set; }
	    /// <summary>
	    /// 是否激活
	    /// </summary>	
	    public bool Enabled { get; set; }
	    /// <summary>
	    /// 更新时间
	    /// </summary>	
	    public DateTime UpdateDate { get; set; }
 

    }
}
	