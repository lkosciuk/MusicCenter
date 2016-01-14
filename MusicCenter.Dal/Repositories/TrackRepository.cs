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
    public static class TrackRepository
    {
        public static IQueryable<Track> GetSinglesByBandName(this IRepository<Track> repository, string BandName, params Expression<Func<Track, object>>[] includes)
        {
            return repository.Queryable().IncludeAll(includes).Where(t => t.band.name == BandName && t.IsSingle);
        }
    }
}
