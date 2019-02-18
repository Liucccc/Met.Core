using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Met.Core.Admin
{
    public static class ComboBoxHelper
    {
        /// <summary>
        /// 下拉菜单
        /// 通用方法
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">控件ID</param>
        /// <param name="url">获取数据地址</param>
        /// <param name="param">参数</param>
        /// <param name="KeyValue">Value值</param>
        /// <param name="KeyText">Text值</param>
        /// <param name="description">描述</param>
        /// <param name="allowSearch">搜索功能</param>
        /// <returns></returns>
        public static HtmlString Multiselect(this IHtmlHelper helper, string id, string url, string param, string KeyValue, string KeyText, string description, bool allowSearch)
        {
            string str = @"
                         <select id='" + id + @"' style='width:100%' class='form-control multiselect'  multiple='multiple'></select>
                         <script>
                            refreshMultiSelect" + id + @"();
                            function refreshMultiSelect" + id + @"() {
                                $.ajax({
                                    type: 'get',
                                    url: '" + url + @"',
                                    data:  " + param + @",
                                    dataType: 'json',
                                    success: function (json) {
                                        $('#" + id + @"').html('');
                                        for (var i = 0; i < json.length; i++) {
                                            $('#" + id + @"').append('<option value=' + json[i]." + KeyValue + @" + '>' + json[i]." + KeyText + @" + ' </option>');
                                        }
                                        $('#" + id + @"').multiselect('destroy').multiselect({
                                            nonSelectedText: '" + description + @"',
                                            selectAllText: '全选',
                                            maxHeight: 120,
                                            buttonWidth: '100%',                                            
                                            includeSelectAllOption: true
                                        });
                                    }
                                });
                            }
                        </script>
            ";
            return new HtmlString(str);
        }

        /// <summary>
        /// 下拉菜单
        /// 通用方法
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">控件ID</param>
        /// <param name="url">获取数据地址</param>
        /// <param name="param">参数</param>
        /// <param name="KeyValue">Value值</param>
        /// <param name="KeyText">Text值</param>
        /// <param name="description">描述</param>
        /// <param name="allowSearch">搜索功能</param>
        /// <returns></returns>
        public static HtmlString ComboBox(this IHtmlHelper helper, string id, string url, string param, string KeyValue, string KeyText, string description, bool allowSearch, bool isNotNull = false)
        {
            string strNotNull = isNotNull ? " isvalid='yes' checkexpession='NotNull' " : " ";
            string str = @"
                <div id='" + id + @"' name='" + id + @"' type='select' class='ui-select' " + strNotNull + @"></div>
                <script>
                    $('#" + id + @"').ComboBox({
                        url: '" + url + @"',
                        param: " + param + @",
                        id: '" + KeyValue + @"',
                        text: '" + KeyText + @"',
                        description: '" + description + @"',
                        allowSearch: " + allowSearch.ToString().ToLower() + @"
                    });
                </script>
            ";
            return new HtmlString(str);
        }

        public static HtmlString ComboBox(this IHtmlHelper helper, string id, object data, string KeyValue, string KeyText, string description, bool allowSearch, bool isNotNull = false)
        {
            string strNotNull = isNotNull ? " isvalid='yes' checkexpession='NotNull' " : " ";
            string str = @"
                <div id='" + id + @"' name='" + id + @"' type='select' class='ui-select' " + strNotNull + @"></div>
                <script>
                    $('#" + id + @"').ComboBox({
                        data: " + data + @",
                        id: '" + KeyValue + @"',
                        text: '" + KeyText + @"',
                        description: '" + description + @"',
                        allowSearch: " + allowSearch.ToString().ToLower() + @"
                    });
                </script>
            ";
            return new HtmlString(str);
        }
    }
}
