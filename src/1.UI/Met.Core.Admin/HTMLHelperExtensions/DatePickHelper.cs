using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Met.Core.Admin
{
    public static class DatePickHelper
    {
        public static HtmlString DatePicker(this IHtmlHelper helper, string id, string value = "", bool Enabled = true, bool IsNotNull = false)
        {

            string strNotNull = IsNotNull ? " isvalid='yes' checkexpession='NotNull' " : " ";
            string strEnabled = Enabled ? " " : " disabled=\"disabled\" readonly=\"readonly\" ";
            string strDate = "";
            DateTime dt;
            if (value != "")
            {
                DateTime.TryParse(value, out dt);
                strDate = dt.ToString("yyyy-MM-dd");
            }

            string str = @"
                <input id=""" + id + @""" type=""text"" value=""" + strDate + @""" class=""form-control input-wdatepicker"" onfocus=""WdatePicker()"" " + strNotNull + strEnabled + @" />
            ";
            return new HtmlString(str);
        }
    }
}
