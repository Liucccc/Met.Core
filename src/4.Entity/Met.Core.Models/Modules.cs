//--------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2019-02-15 11:29:25 
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//--------------------------------------------------------------------
using System;

namespace Met.Core.Models
{	
	/// <summary>
	/// Modules
	/// </summary>	
	public partial class Modules
	{
	    /// <summary>
	    /// 
	    /// </summary>	
	    public int Id { get; set; }
	    /// <summary>
	    /// 父模块Id
	    /// </summary>	
	    public int? ParentId { get; set; }
	    /// <summary>
	    /// 名称
	    /// </summary>	
	    public string Name { get; set; }
	    /// <summary>
	    /// 链接地址
	    /// </summary>	
	    public string LinkUrl { get; set; }
	    /// <summary>
	    /// 是否是菜单
	    /// </summary>	
	    public bool IsMenu { get; set; }
	    /// <summary>
	    /// 模块编号
	    /// </summary>	
	    public int Code { get; set; }
	    /// <summary>
	    /// 描述
	    /// </summary>	
	    public string Description { get; set; }
	    /// <summary>
	    /// 是否激活
	    /// </summary>	
	    public bool Enabled { get; set; }
	    /// <summary>
	    /// 更新时间
	    /// </summary>	
	    public DateTime UpdateDate { get; set; }
	    /// <summary>
	    /// 
	    /// </summary>	
	    public string Icon { get; set; }
 

    }
}
	