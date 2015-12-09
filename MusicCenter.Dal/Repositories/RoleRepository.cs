using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.Repositories
{
    public static class RoleRepository
    {
        public static Role GetRoleByName(this IRepository<Role> repo, string name)
        {
            return repo.Queryable().FirstOrDefault(r => r.Name == name);
        }
    }
}
