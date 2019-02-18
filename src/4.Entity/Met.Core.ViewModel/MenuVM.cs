using System;
using System.Collections.Generic;
using System.Text;

namespace Met.Core.ViewModel
{
    public class MenuVM
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 父模块Id
        /// </summary>	
        public int? ParentId { get; set; }
        /// <summary>
        /// 是否默认展开
        /// </summary>
        public bool isOpen { get; set; }
        /// <summary>
        /// 名称
        /// </summary>	
        public string text { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>	
        public string url { get; set; }
        /// <summary>
        /// 打开方式 默认iframe-tab
        /// </summary>
        public string targetType { get; set; }
        /// <summary>
        /// 是否是菜单
        /// </summary>	
        public bool IsMenu { get; set; }
        /// <summary>
        /// 模块编号
        /// </summary>	
        public int Code { get; set; }
        /// <summary>
        /// 子模块
        /// </summary>
        public List<MenuVM> children { get; set; }
    }
}
