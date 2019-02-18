using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Met.Core.IServices;
using Met.Core.Models;
using Met.Core.Util.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Met.Core.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRolesServices _rolesServices;
        public RoleController(IRolesServices rolesServices)
        {
            _rolesServices = rolesServices;
        }

        #region 查询
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTableJson(Pagination pagination, string roleName, int? enable)
        {
            Expression<Func<Roles, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(roleName))
            {
                wh = wh.And(c => c.RoleName.Contains(roleName));
            }
            if (enable != null)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var data = await _rolesServices.QueryPage(wh, pagination.page, pagination.rows, pagination.sortOrder);
            return Json(new { total = data.records, rows = data });
        }

        #endregion

        #region 添加修改
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Roles roles)
        {
            bool res = false;
            roles.UpdateDate = DateTime.Now;
            if (roles.Id > 0)
                res = await _rolesServices.Update(roles);
            else
            {
                var data = await _rolesServices.QueryByClause(m => m.RoleName == roles.RoleName);
                if (data != null)
                    return Error(roles.RoleName + "已经存在！");
                res = await _rolesServices.Add(roles);
            }
            if (res)
                return Success("保存成功");
            else
                return Error("保存失败");
        }

        public async Task<IActionResult> GetFromJson(int? id)
        {
            var data = await _rolesServices.QueryByID(id);
            return Json(data); ;
        }
        #endregion

        #region 删除
        public async Task<IActionResult> Del(string ids)
        {
            var res = await _rolesServices.DeleteByIds(ids.Split(','));
            if (res)
                return Success("删除成功！");
            return Error("删除失败！");
        }
        #endregion

        #region 授权
        public IActionResult Authorize()
        {
            return View();
        }
        #endregion
    }
}