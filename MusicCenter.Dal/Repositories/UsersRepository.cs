﻿using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Dal.RepoExt;
using System.Data.Entity;
using System.Linq.Expressions;
using MusicCenter.Common.Extensions;

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
            return repo.Queryable().Any(u => u.email.ToLower().Equals(email.ToLower()));                     
        }

        public static Users GetUserByEmail(this IRepository<Users> repo, string email, params Expression<Func<Users, object>>[] includes)
        {
            return repo.Queryable().IncludeAll(includes).FirstOrDefault(u => u.email.ToLower() == email.ToLower());
        }

    }
}
