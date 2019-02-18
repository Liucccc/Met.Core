using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Met.Core.IServices;
using Met.Core.Models;
using Met.Core.Util;
using Met.Core.Util.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Met.Core.Admin.Controllers
{
    public class UserGroupsController : BaseController
    {
        private readonly IUserGroupsServices _userGroupsServices;
        private readonly IRolesServices _rolesServices;
        private readonly IUserGroupRolesServices _userGroupRolesServices;
        public UserGroupsController(IUserGroupsServices userGroupsServices, IRolesServices rolesServices, IUserGroupRolesServices userGroupRolesServices)
        {
            _userGroupsServices = userGroupsServices;
            _rolesServices = rolesServices;
            _userGroupRolesServices = userGroupRolesServices;
        }

        #region 查询
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTableJson(Pagination pagination, string groupName, int? enable)
        {
            Expression<Func<UserGroups, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(groupName))
            {
                wh = wh.And(c => c.GroupName.Contains(groupName));
            }
            if (enable != null)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var data = await _userGroupsServices.QueryPage(wh, pagination.page, pagination.rows, pagination.sortOrder);
            return Json(new { total = data.records, rows = data });
        }

        #endregion

        #region 添加修改
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserGroups UserGroups)
        {
            bool res = false;
            UserGroups.UpdateDate = DateTime.Now;
            if (UserGroups.Id > 0)
                res = await _userGroupsServices.Update(UserGroups);
            else
            {
                var data = await _userGroupsServices.QueryByClause(m => m.GroupName == UserGroups.GroupName);
                if (data != null)
                    return Error(UserGroups.GroupName + "已经存在！");
                res = await _userGroupsServices.Add(UserGroups);
            }
            if (res)
                return Success("保存成功");
            else
                return Error("保存失败");
        }

        public async Task<IActionResult> GetFromJson(int? id)
        {
            var data = await _userGroupsServices.QueryByID(id);
            return Json(data); ;
        }
        #endregion

        #region 删除
        public async Task<IActionResult> Del(string ids)
        {
            var res = await _userGroupsServices.DeleteByIds(ids.Split(','));
            if (res)
                return Success("删除成功！");
            return Error("删除失败！");
        }
        #endregion

        #region 设置角色
        public async Task<IActionResult> SetRole(int id)
        {
            var allRoles = await _rolesServices.Query(r => r.Enabled, s => s.OrderSort, true);
            var groupRolesIds = await Task.Run(() => _userGroupRolesServices.Query(m => m.UserGroup_Id == id).Result.Select(r => r.Role_Id).Distinct().ToList());
            ViewBag.allRoles = allRoles;
            ViewBag.groupRolesIds = groupRolesIds;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetRole(int id, string checkIds)
        {
            var res = await _userGroupRolesServices.Delete(m => m.UserGroup_Id == id);
            if (!string.IsNullOrEmpty(checkIds))
            {
                List<UserGroupRoles> list = new List<UserGroupRoles>();
                var checkId = checkIds.Split(',');
                foreach (var item in checkId)
                {
                    list.Add(new UserGroupRoles
                    {
                        Role_Id = item.ToInt(),
                        UserGroup_Id = id
                    });
                }
                res = await _userGroupRolesServices.Add(list);
            }
            if (res)
                return Success("保存成功");
            return Error("设置角色失败");
        }
        #endregion
    }
}