using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Met.Core.IServices;
using Newtonsoft.Json;
using Met.Core.ViewModel;

namespace Met.Core.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUsersServices _usersServices;
        private readonly IModulesServices _modulesServices;
        private readonly IAccountServices _accountServices;
        public AccountController(IUsersServices usersServices, IModulesServices modulesServices, IAccountServices accountServices)
        {
            _usersServices = usersServices;
            _modulesServices = modulesServices;
            _accountServices = accountServices;
        }
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            //var user = await _usersServices.Login(loginVM);
            var user = await _accountServices.Login(loginVM);
            if (user != null)
            {
                CurrentUser = user;
                SetCookies(currentUserKey, JsonConvert.SerializeObject(user), 7200);
                return Success();
            }
            else
                return Error("账号密码错误");

        }

        /// <summary>
        /// 获得菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetMenu()
        {
            var menu = await _accountServices.GetMenu(CurrentUser.Id);
            return Success("", menu);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public IActionResult OutLogin()
        {
            DeleteCookies(currentUserKey);
            return Success();
        }
    }
}