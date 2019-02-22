
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// UserGroupsServices
	/// </summary>	
	public class UserGroupsServices : BaseServices<UserGroups>, IUserGroupsServices
    {
	
        IUserGroupsRepository dal;
        public UserGroupsServices(IUserGroupsRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	