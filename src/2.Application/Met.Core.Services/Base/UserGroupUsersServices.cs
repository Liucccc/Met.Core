
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// UserGroupUsersServices
	/// </summary>	
	public class UserGroupUsersServices : BaseServices<UserGroupUsers>, IUserGroupUsersServices
    {
	
        IUserGroupUsersRepository dal;
        public UserGroupUsersServices(IUserGroupUsersRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	