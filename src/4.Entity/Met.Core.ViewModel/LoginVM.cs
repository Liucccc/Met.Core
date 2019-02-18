using System;
using System.Collections.Generic;
using System.Text;

namespace Met.Core.ViewModel
{
    public class LoginVM
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否记住登录
        /// </summary>
        public bool IsRememberLogin { get; set; }
    }
}
