using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Infrastructure;

namespace MusicCenter.Dal.Repositories
{
    public static class RoleRepository
    {
        public static Role GetRoleByName(this IRepository<Role> repo , string RoleName)
        {
            return repo.Queryable().FirstOrDefault(r => r.Name == RoleName);
        }
    }
}
