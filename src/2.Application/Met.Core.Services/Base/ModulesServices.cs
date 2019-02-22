
using System;
using Met.Core.IServices;
using Met.Core.IRepository;
using Met.Core.Models;

namespace Met.Core.Services
{	
	/// <summary>
	/// ModulesServices
	/// </summary>	
	public class ModulesServices : BaseServices<Modules>, IModulesServices
    {
	
        IModulesRepository dal;
        public ModulesServices(IModulesRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
       
    }
}

	