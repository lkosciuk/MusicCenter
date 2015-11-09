using MusicCenter.Dal.EntityModels;
using MusicCenter.Services.Intefaces;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Infrastructure;
using MusicCenter.Dal.Repositories;

namespace MusicCenter.Services.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IUnitOfWork u)
            : base(u)
        {

        }

        public Role AddRole(string RoleName)
        {
            if (!IfRoleExists(RoleName))
            {
                Role newRole = new Role()
                {
                    Name = RoleName,
                    ObjectState = ObjectState.Added
                };

                _repo.Insert(newRole);
                _unitOfWork.SaveChanges();

                return newRole;  
            }

            return null;
            
        }

        public bool IfRoleExists(string RoleName)
        {
            return _repo.Queryable().Any(r => r.Name == RoleName);
        }

        public Role GetRoleByName(string RoleName)
        {
            return _repo.GetRoleByName(RoleName);
        }
    }
}
