
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// UsersServices
	/// </summary>	
	public class UsersServices : BaseServices<Users>, IUsersServices
    {
	
        IUsersRepository dal;
        public UsersServices(IUsersRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	