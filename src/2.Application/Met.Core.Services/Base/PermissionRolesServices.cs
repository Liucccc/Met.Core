
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// PermissionRolesServices
	/// </summary>	
	public class PermissionRolesServices : BaseServices<PermissionRoles>, IPermissionRolesServices
    {
	
        IPermissionRolesRepository dal;
        public PermissionRolesServices(IPermissionRolesRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	