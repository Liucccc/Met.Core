using Met.Core.Models;
using Met.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Met.Core.IServices
{
    public interface IAccountServices
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        Task<Users> Login(LoginVM loginVM);
        /// <summary>
        /// 获得用户菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<MenuVM>> GetMenu(int userId);
    }
}
