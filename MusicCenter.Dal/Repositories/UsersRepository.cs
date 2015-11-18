using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Dal.RepoExt;
using System.Data.Entity;

namespace MusicCenter.Dal.Repositories
{
    public static class UsersRepository
    {
        public static Users GetUserById(this IRepository<Users> repository, int userId)
        {
            
            return repository.GetById(userId);
        }

        public static bool IsUserExists(this IRepository<Users> repo, string email)
        {
            if (repo.Queryable().Any(u => u.email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }

            return false;
        }

        public static Users GetUserByEmail(this IRepository<Users> repo, string email)
        {
            return repo.Queryable().Include(d => d.favourites)
                   .Include(d => d.profilePhoto)
                   .Include(d => d.roles)
                   .FirstOrDefault(u => u.email.ToLower() == email.ToLower());
        }
    }
}
