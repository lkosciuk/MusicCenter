using MusicCenter.Dal.EntityModels;
using MusicCenter.Dal.RepoExt;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.Repositories
{
    public static class AlbumRepository
    {
        public static IQueryable<Album> GeAlbumsByBandName(this IRepository<Album> repository, string BandName, params Expression<Func<Album, object>>[] includes)
        {
            return repository.Queryable().IncludeAll(includes).Where(b => b.band.name == BandName).AsQueryable();
        }

        public static IQueryable<Album> GetAlbumByName(this IRepository<Album> repo, string AlbumName, params Expression<Func<Album, object>>[] includes)
        {
            return repo.Queryable().IncludeAll(includes).Where(a => a.name == AlbumName);
        }

        public static IQueryable<Album> GetNewestAlbums(this IRepository<Album> repo, params Expression<Func<Album, object>>[] includes)
        {
            return repo.Queryable().IncludeAll(includes).OrderByDescending(b => b.releaseDate).Take(3);
        }
    }
}
