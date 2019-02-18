using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;
using Met.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Met.Core.Services
{
    public class AccountServices : IAccountServices
    {
        IUsersRepository _users_dal;
        IModulesRepository _modules_dal;
        IRolesRepository _role_dal;
        IRoleUsersRepository _roleUsers_dal;
        IUserGroupsRepository _userGroups_dal;
        IUserGroupRolesRepository _userGroupRoles_dal;
        IUserGroupUsersRepository _userGroupUsers_dal;
        IPermissionRolesRepository _permissionRoles_dal;
        IPermissionsRepository _permissions_dal;
        public AccountServices(
            IUsersRepository users_dal,
            IModulesRepository modules_dal,
            IRolesRepository role_dal,
            IRoleUsersRepository roleUsers_dal,
            IUserGroupsRepository userGroups_dal,
            IUserGroupRolesRepository userGroupRoles_dal,
            IUserGroupUsersRepository userGroupUsers_dal,
            IPermissionRolesRepository permissionRoles_dal,
            IPermissionsRepository permissions_dal
            )
        {
            _users_dal = users_dal;
            _modules_dal = modules_dal;
            _role_dal = role_dal;
            _roleUsers_dal = roleUsers_dal;
            _userGroups_dal = userGroups_dal;
            _userGroupRoles_dal = userGroupRoles_dal;
            _userGroupUsers_dal = userGroupUsers_dal;
            _permissionRoles_dal = permissionRoles_dal;
            _permissions_dal = permissions_dal;
        }

        public async Task<Users> Login(LoginVM loginVM)
        {
            return await _users_dal.QueryByClause(u => u.UserName == loginVM.LoginName && u.Password == Util.Helpers.Encrypt.Md5By32(loginVM.Password) && u.Enabled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<MenuVM>> GetMenu(int userId)
        {
            List<MenuVM> parentMenuList = new List<MenuVM>();
            //获得用户直接角色
            var roleIds = await Task.Run(() => _roleUsers_dal.Query(r => r.User_Id == userId).Result.Select(r => r.Role_Id).ToList());
            //获得用户组
            var userGroupIds = await Task.Run(() => _userGroupUsers_dal.Query(m => m.User_Id == userId).Result.Select(g => g.UserGroup_Id).ToList());
            //获得用户组角色
            var userGroupRoleIds = await Task.Run(() => _userGroupRoles_dal.Query(r => userGroupIds.Contains(r.UserGroup_Id)).Result.Select(r => r.Role_Id).ToList());
            //获得用户总角色
            roleIds.AddRange(userGroupRoleIds);
            var roleIdsByUser = roleIds.Distinct().ToList();
            //获得用户总角色权限
            var permissionIds = await Task.Run(() => _permissionRoles_dal.Query(p => roleIdsByUser.Contains(p.Role_Id)).Result.Select(p => p.Permission_Id).Distinct().ToList());
            var permissions = await Task.Run(() => _permissions_dal.Query(p => p.Enabled && permissionIds.Contains(p.Id)));
            //获得用户权限菜单
            var ModuleIds = permissions.Select(p => p.ModuleId).Distinct().ToList();
            var childModules = await Task.Run(() => _modules_dal.Query(m => ModuleIds.Contains(m.Id)));
            if (childModules.Count > 0)
            {
                var ParentIds = childModules.Select(m => m.ParentId).Distinct().ToList();
                var parentMenu = await Task.Run(() => _modules_dal.Query(m => ParentIds.Contains(m.Id)).Result);
                foreach (var item in parentMenu)
                {
                    MenuVM menu = new MenuVM();
                    menu.id = item.Id;
                    menu.isOpen = false;
                    menu.text = item.Name;
                    menu.icon = item.Icon;
                    menu.children = new List<MenuVM>();
                    foreach (var child in childModules)
                    {
                        MenuVM childMenu = new MenuVM();
                        if (child.ParentId == item.Id)
                        {
                            childMenu.id = child.Id;
                            childMenu.isOpen = false;
                            childMenu.url = child.LinkUrl;
                            childMenu.text = child.Name;
                            childMenu.targetType = "iframe-tab";
                            childMenu.icon = child.Icon;
                            menu.children.Add(childMenu);
                        }
                    }
                    parentMenuList.Add(menu);
                }
            }

            return parentMenuList;

        }
    }
}
