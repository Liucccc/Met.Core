using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Met.Core.Models;
using Met.Core.Util.Helpers;
using Microsoft.AspNetCore.Mvc;
using Met.Core.IServices;
using Met.Core.Util;

namespace Met.Core.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersServices _usersServices;
        private readonly IUserGroupsServices _userGroupsServices;
        private readonly IUserGroupUsersServices _userGroupUsersServices;
        private readonly IRolesServices _rolesServices;
        private readonly IRoleUsersServices _roleUsersServices;
        public UsersController(IUsersServices usersServices,
            IUserGroupsServices userGroupsServices,
            IUserGroupUsersServices userGroupUsersServices,
            IRolesServices rolesServices,
            IRoleUsersServices roleUsersServices)
        {
            _usersServices = usersServices;
            _userGroupsServices = userGroupsServices;
            _userGroupUsersServices = userGroupUsersServices;
            _rolesServices = rolesServices;
            _roleUsersServices = roleUsersServices;
        }

        #region 查询
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTableJson(Pagination pagination, string userName, int? enable)
        {
            Expression<Func<Users, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(userName))
            {
                wh = wh.And(c => c.TrueName.Contains(userName));
            }
            if (enable != null)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var data = await _usersServices.QueryPage(wh, pagination.page, pagination.rows, pagination.sortOrder);
            return Json(new { total = data.records, rows = data });
        }

        #endregion

        #region 添加修改
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Users users)
        {
            bool res = false;
            users.UpdateDate = DateTime.Now;
            if (users.Id > 0)
            {
                var data = await _usersServices.QueryByID(users.Id);
                if (data == null)
                    return Error("修改用户失败，未找到该用户。");
                else
                    res = await _usersServices.Update(users, null, new List<string> { "Password" });
            }
            else
            {
                var data = await _usersServices.QueryByClause(m => m.UserName == users.UserName);
                if (data != null)
                    return Error("登录姓名：" + users.UserName + "已经存在！");
                users.Password = Util.Helpers.Encrypt.Md5By32("123456");
                res = await _usersServices.Add(users);
            }
            if (res)
                return Success("保存成功");
            else
                return Error("保存失败");
        }

        public async Task<IActionResult> GetFromJson(int? id)
        {
            var data = await _usersServices.QueryByID(id);
            return Json(data); ;
        }
        #endregion

        #region 删除
        public async Task<IActionResult> Del(string ids)
        {
            var res = await _usersServices.DeleteByIds(ids.Split(','));
            if (res)
                return Success("删除成功！");
            return Error("删除失败！");
        }
        #endregion

        #region 重置密码
        public async Task<IActionResult> ResetPwd(int id)
        {
            bool res = false;
            var user = await _usersServices.QueryByID(id);
            if (user != null)
            {
                user.Password = Util.Helpers.Encrypt.Md5By32("123456");
                res = await _usersServices.Update(user);
                if (res)
                    return Success("重置密码成功");
            }
            return Error("重置密码失败，未找到该用户。");
        }
        #endregion

        #region 设置用户组
        public async Task<IActionResult> SetGroup(int id)
        {
            var allGroups = await _userGroupsServices.Query(g => g.Enabled, g => g.OrderSort, true);
            var userGroupsIds = await Task.Run(() => _userGroupUsersServices.Query(m => m.User_Id == id).Result.Select(r => r.UserGroup_Id).ToList());
            ViewBag.allGroups = allGroups;
            ViewBag.userGroupsIds = userGroupsIds;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SetGroup(int id, string checkIds)
        {
            var res = await _userGroupUsersServices.Delete(m => m.User_Id == id);
            if (!string.IsNullOrEmpty(checkIds))
            {
                List<UserGroupUsers> list = new List<UserGroupUsers>();
                var checkId = checkIds.Split(',');
                foreach (var item in checkId)
                {
                    list.Add(new UserGroupUsers
                    {
                        UserGroup_Id = item.ToInt(),
                        User_Id = id
                    });
                }
                res = await _userGroupUsersServices.Add(list);
            }
            if (res)
                return Success("保存成功");
            return Error("设置用户组失败");
        }
        #endregion

        #region 设置角色
        public async Task<IActionResult> SetRole(int id)
        {
            var allRoles = await _rolesServices.Query(r => r.Enabled, s => s.OrderSort, true);
            var userRolesIds = await Task.Run(() => _roleUsersServices.Query(m => m.User_Id == id).Result.Select(r => r.Role_Id).Distinct().ToList());
            ViewBag.allRoles = allRoles;
            ViewBag.userRolesIds = userRolesIds;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetRole(int id,string checkIds)
        {
            var res = await _roleUsersServices.Delete(m => m.User_Id == id);
            if (!string.IsNullOrEmpty(checkIds))
            {
                List<RoleUsers> list = new List<RoleUsers>();
                var checkId = checkIds.Split(',');
                foreach (var item in checkId)
                {
                    list.Add(new RoleUsers
                    {
                        Role_Id = item.ToInt(),
                        User_Id = id
                    });
                }
                res = await _roleUsersServices.Add(list);
            }
            if (res)
                return Success("保存成功");
            return Error("设置角色失败");
        }
        #endregion
    }
}