using System;
using System.Collections.Generic;
using System.Text;

namespace Met.Core.Models
{/// <summary>
 /// 分页参数
 /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string sortOrder
        {
            get
            {
                if (!string.IsNullOrEmpty(sort))
                    return sort + " " + (!string.IsNullOrEmpty(order) ? order : "");
                else
                    return "";
            }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total
        {
            get
            {
                if (records > 0)
                {
                    return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 查询条件Json
        /// </summary>
        public string conditionJson { get; set; }
    }
}
