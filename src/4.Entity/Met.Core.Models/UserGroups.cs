//--------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2019-02-15 11:29:25 
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//--------------------------------------------------------------------
using System;

namespace Met.Core.Models
{	
	/// <summary>
	/// UserGroups
	/// </summary>	
	public partial class UserGroups
	{
	    /// <summary>
	    /// 
	    /// </summary>	
	    public int Id { get; set; }
	    /// <summary>
	    /// 用户组名称
	    /// </summary>	
	    public string GroupName { get; set; }
	    /// <summary>
	    /// 描述
	    /// </summary>	
	    public string Description { get; set; }
	    /// <summary>
	    /// 排序
	    /// </summary>	
	    public int OrderSort { get; set; }
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
	