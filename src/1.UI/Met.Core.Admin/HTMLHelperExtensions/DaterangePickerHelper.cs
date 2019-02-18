using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Met.Core.Admin
{
    public static class DaterangePickerHelper
    {
        /// <summary>
        /// 日期段
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">控件ID</param>
        /// <param name="placeholder">显示名称</param>
        /// <param name="dr">默认显示日期段</param>
        /// <returns></returns>
        public static HtmlString DaterangePicker(this IHtmlHelper helper, string id, string placeholder = "", Daterange dr = Daterange.CustomRange, string DefaultValue = "")
        {
            string value = "";
            string strSplit = " 至 ";
            //string strFormart = "yyyy-MM-dd";
            switch (dr)
            {
                case Daterange.Today:
                    value = System.DateTime.Now.ToString("yyyy-MM-dd") + strSplit + System.DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case Daterange.Yesterday:
                    value = System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + strSplit + System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case Daterange.Last7Days:
                    value = System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd") + strSplit + System.DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case Daterange.Last30Days:
                    value = System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd") + strSplit + System.DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case Daterange.ThisMonth:
                    value = System.DateTime.Now.AddDays(-1 * System.DateTime.Now.Day).ToString("yyyy-MM-dd") + strSplit + System.DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case Daterange.LastMonth:
                    value = System.DateTime.Now.AddMonths(-1).Year.ToString() + "-" + System.DateTime.Now.AddMonths(-1).Month.ToString() + "-01" + strSplit + System.DateTime.Now.AddDays(-1 * System.DateTime.Now.Day).ToString();
                    break;
                case Daterange.CustomRange:
                    value = DefaultValue;
                    break;
                default:
                    value = "";
                    break;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<input type=\"text\" class=\"form-control pull-right\" id=\"" + id + "\" name=\"" + id + "\" placeholder=\"" + placeholder + "\" colname=\"" + placeholder + "\" value=\"" + value + "\">");
            //sb.Append("<input type=\"text\" class=\"form-control pull-right\" id=\"" + id + "\" name=\"" + id + "\" value=\"" + value + "\">");
            sb.Append("<script type=\"text/javascript\">$('#" + id + "').daterangepicker();</script>");

            return new HtmlString(sb.ToString());
        }
    }

    public enum Daterange
    {
        Today = 0,
        Yesterday = 1,
        Last7Days = 2,
        Last30Days = 3,
        ThisMonth = 4,
        LastMonth = 5,
        CustomRange = 6
    }
}
