
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// UserGroupRolesServices
	/// </summary>	
	public class UserGroupRolesServices : BaseServices<UserGroupRoles>, IUserGroupRolesServices
    {
	
        IUserGroupRolesRepository dal;
        public UserGroupRolesServices(IUserGroupRolesRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	