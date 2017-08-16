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
            return repository.Queryable().IncludeAll(includes).Where(b => b.ConcertOwner.name == BandName || b.bands.Any(c => c.name == BandName)).AsQueryable();
        }

        public static IQueryable<Concert> GetAllConcertsInMonth(this IRepository<Concert> repository,int year, int month, params Expression<Func<Concert, object>>[] includes)
        {
            return repository.Queryable().IncludeAll(includes).Where(c => c.date.Year == year && c.date.Month == month).AsQueryable();
        }

        public static IQueryable<Concert> GetConcertById(this IRepository<Concert> repository, int id, params Expression<Func<Concert, object>>[] includes)
        {
            return repository.Queryable().IncludeAll(includes).Where(c => c.Id == id);
        }
    }
}
