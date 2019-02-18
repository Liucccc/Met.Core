using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Met.Core.IServices;
using Met.Core.Models;
using Met.Core.Util.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Met.Core.Admin.Controllers
{
    public class ModulesController : BaseController
    {
        private readonly IModulesServices _ModulesServices;
        public ModulesController(IModulesServices ModulesServices)
        {
            _ModulesServices = ModulesServices;
        }

        #region 查询
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTableJson(Pagination pagination, string name, int? enable)
        {
            Expression<Func<Modules, bool>> wh = c => true;
            if (!string.IsNullOrEmpty(name))
            {
                wh = wh.And(c => c.Name.Contains(name));
            }
            if (enable != null)
            {
                var yesOrNot = enable != 0;
                wh = wh.And(c => c.Enabled == yesOrNot);
            }
            var data = await _ModulesServices.QueryPage(wh, pagination.page, pagination.rows, pagination.sortOrder);
            return Json(new { total = data.records, rows = data });
        }

        #endregion

        #region 添加修改
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Modules Modules)
        {
            bool res = false;
            Modules.UpdateDate = DateTime.Now;
            if (Modules.Id > 0)
            {
                res = await _ModulesServices.Update(Modules);
            }
            else
            {
                res = await _ModulesServices.Add(Modules);
            }
            if (res)
                return Success("保存成功");
            else
                return Error("保存失败");
        }

        public async Task<IActionResult> GetFromJson(int? id)
        {
            var data = await _ModulesServices.QueryByID(id);
            return Json(data); ;
        }
        #endregion
    }
}