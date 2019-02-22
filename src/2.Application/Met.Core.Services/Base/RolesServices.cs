
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// RolesServices
	/// </summary>	
	public class RolesServices : BaseServices<Roles>, IRolesServices
    {
	
        IRolesRepository dal;
        public RolesServices(IRolesRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	