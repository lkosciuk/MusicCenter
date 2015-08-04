using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.Repositories
{
    public static class UsersRepository
    {
        public static Users GetUserById(this IRepository<Users> repository, int userId)
        {
            
            return repository.GetUserById(userId);
        }
    }
}
