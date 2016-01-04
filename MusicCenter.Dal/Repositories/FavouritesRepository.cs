using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Dal.RepoExt;

namespace MusicCenter.Dal.Repositories
{
    public static class FavouritesRepository
    {
        public static IQueryable<Favourites> GetUserFavourites(this IRepository<Favourites> repository, string email, params Expression<Func<Favourites, object>>[] includes)
        {
            return repository.Queryable().IncludeAll(includes).Where(f => f.user.email == email);
        }
    }
}
