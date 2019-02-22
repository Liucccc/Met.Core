
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// PermissionsServices
	/// </summary>	
	public class PermissionsServices : BaseServices<Permissions>, IPermissionsServices
    {
	
        IPermissionsRepository dal;
        public PermissionsServices(IPermissionsRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	