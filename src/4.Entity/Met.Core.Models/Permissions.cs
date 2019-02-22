using System;

namespace Met.Core.Models
{	
	/// <summary>
	/// Permissions
	/// </summary>	
	public partial class Permissions
	{
	    /// <summary>
	    /// 
	    /// </summary>	
	    public int Id { get; set; }
	    /// <summary>
	    /// 名称
	    /// </summary>	
	    public string Name { get; set; }
	    /// <summary>
	    /// 权限编码
	    /// </summary>	
	    public string Code { get; set; }
	    /// <summary>
	    /// 描述
	    /// </summary>	
	    public string Description { get; set; }
	    /// <summary>
	    /// 是否激活
	    /// </summary>	
	    public bool Enabled { get; set; }
	    /// <summary>
	    /// 所属模块Id
	    /// </summary>	
	    public int ModuleId { get; set; }
	    /// <summary>
	    /// 更新时间
	    /// </summary>	
	    public DateTime UpdateDate { get; set; }
 

    }
}
	