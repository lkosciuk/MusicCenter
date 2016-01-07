using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MusicCenter.Dal.RepoExt;

namespace MusicCenter.Dal.Repositories
{
    public static class GenreRepository
    {
        public static Genre GetGenreByName(this IRepository<Genre> repo, string name, params Expression<Func<Genre, object>>[] includes)
        {
            return repo.Queryable().IncludeAll(includes).FirstOrDefault(g => g.name.ToLower() == name.ToLower());
        }

        public static bool IsGenreExists(this IRepository<Genre> repo, string name)
        {
            return repo.Queryable().Any(g => g.name.ToLower() == name.ToLower());
        }
    }
}
