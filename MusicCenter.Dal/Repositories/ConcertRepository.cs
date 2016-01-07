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
    public static class ConcertRepository
    {
        public static IQueryable<Concert> GetBandConcerts(this IRepository<Concert> repository, string BandName, params Expression<Func<Concert, object>>[] includes)
        {
            return repository.Queryable().IncludeAll(includes).Where(b => b.bands.Any(c => c.name == BandName)).AsQueryable();
        }
    }
}
