using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Met.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Met.Core.Admin.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : Controller
    {
        protected string currentUserKey = "E8370C80395D41E0A44C7588AD4DC2C2";
        /// <summary>
        /// 当前登录用户
        /// </summary>
        private Users _currentUser;
        public Users CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    string cookie = GetCookies(currentUserKey);
                    if (cookie != null && cookie != "")
                    {
                        _currentUser = JsonConvert.DeserializeObject<Users>(cookie);
                    }
                    else
                    {
                        _currentUser = new Users();
                    }
                }
                return _currentUser;
            }
            set
            {
                _currentUser = value;
            }
        }
        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string path = filterContext.HttpContext.Request.Path;
            if (CurrentUser.Id == 0 && path.ToLower() != "/account/login" && path != "/")
            {
                filterContext.Result = new RedirectResult($"/Account/Login?ReturnUrl={path}");
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult Success(string message = "", object data = null)
        {
            return Json(new { code = AjaxCode.success, msg = message, data = data });
        }

        public IActionResult Error(string message, object data = null)
        {
            return Json(new { code = AjaxCode.error, msg = message, data = data });
        }

        #region Cookie
        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        protected void SetCookies(string key, string value, int minutes = 30)
        {
            HttpContext.Response.Cookies.Append(key, Util.Helpers.Encrypt.DesEncrypt(value), new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        protected void DeleteCookies(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return Util.Helpers.Encrypt.DesDecrypt(value);
        }
        #endregion
    }
    /// <summary>
    /// Ajax返回类型
    /// </summary>
    public enum AjaxCode
    {
        /// <summary>
        /// 消息结果类型
        /// </summary>
        info = 0,

        /// <summary>
        /// 成功结果类型
        /// </summary>
        success = 1,

        /// <summary>
        /// 警告结果类型
        /// </summary>
        warning = 2,

        /// <summary>
        /// 异常结果类型
        /// </summary>
        error = -1
    }
}
