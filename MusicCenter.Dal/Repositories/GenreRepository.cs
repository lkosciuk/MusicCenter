using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.Repositories
{
    public static class GenreRepository
    {
        public static Genre GetGenreByName(this IRepository<Genre> repo , string name)
        {
            return repo.Queryable().FirstOrDefault(g => g.name.ToLower() == name.ToLower());
        }

        public static bool IsGenreExists(this IRepository<Genre> repo, string name)
        {
            return repo.Queryable().Any(g => g.name.ToLower() == name.ToLower());
        }
    }
}
