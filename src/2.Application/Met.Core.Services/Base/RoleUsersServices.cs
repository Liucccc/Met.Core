
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// RoleUsersServices
	/// </summary>	
	public class RoleUsersServices : BaseServices<RoleUsers>, IRoleUsersServices
    {
	
        IRoleUsersRepository dal;
        public RoleUsersServices(IRoleUsersRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	