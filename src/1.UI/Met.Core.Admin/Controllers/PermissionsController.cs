using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Met.Core.IServices;
using Met.Core.Models;
using Met.Core.Util.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Met.Core.Admin.Controllers
{
    public class PermissionsController : BaseController
    {
        private readonly IPermissionsServices _permissionsServices;
        public PermissionsController(IPermissionsServices permissionsServices)
        {
            _permissionsServices = permissionsServices;
        }

        #region 查询
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTableJson(Pagination pagination, string name, int? enable)
        {
            Expression<Func<Permissions, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(name))
            {
                wh = wh.And(c => c.Name.Contains(name));
            }
            if (enable != null)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var data = await _permissionsServices.QueryPage(wh, pagination.page, pagination.rows, pagination.sortOrder);
            return Json(new { total = data.records, rows = data });
        }

        #endregion

        #region 添加修改
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Permissions Permissions)
        {
            bool res = false;
            Permissions.UpdateDate = DateTime.Now;
            if (Permissions.Id > 0)
                res = await _permissionsServices.Update(Permissions);
            else
            {
                var data = await _permissionsServices.QueryByClause(m => m.Code == Permissions.Code);
                if (data != null)
                    return Error(Permissions.Code + "已经存在！");
                res = await _permissionsServices.Add(Permissions);
            }
            if (res)
                return Success("保存成功");
            else
                return Error("保存失败");
        }

        public async Task<IActionResult> GetFromJson(int? id)
        {
            var data = await _permissionsServices.QueryByID(id);
            return Json(data); ;
        }
        #endregion

        #region 删除
        public async Task<IActionResult> Del(string ids)
        {
            var res = await _permissionsServices.DeleteByIds(ids.Split(','));
            if (res)
                return Success("删除成功！");
            return Error("删除失败！");
        }
        #endregion

        #region 获得菜单按钮
        public async Task<IActionResult> GetUserPermissionButtonsByModuleId(int moduleId)
        {
            var data = await _permissionsServices.Query(m => m.ModuleId == moduleId && m.Enabled);
            return Json(data);
        }
        #endregion
    }
}