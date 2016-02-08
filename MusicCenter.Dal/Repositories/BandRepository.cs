using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MusicCenter.Dal.RepoExt;
using System.Linq.Expressions;

namespace MusicCenter.Dal.Repositories
{
    public static class BandRepository
    {
        public static IQueryable<Band> GetBandByName(this IRepository<Band> repository, string BandName, params Expression<Func<Band, object>>[] includes)
        {
            return repository.Queryable().IncludeAll(includes).Where(b => b.name == BandName).AsQueryable();
        }

        public static IQueryable<Band> GetBandsByConcert(this IRepository<Band> repo, int ConcertId, params Expression<Func<Band, object>>[] includes)
        {
            return repo.Queryable().IncludeAll(includes).Where(b => b.MemberConcerts.Any(c => c.Id == ConcertId) || b.OwnedConcerts.Any(c => c.Id == ConcertId)).AsQueryable();
        }

        public static IQueryable<Band> GetNewestBands(this IRepository<Band> repo, params Expression<Func<Band, object>>[] includes)
        {
            return repo.Queryable().IncludeAll(includes).OrderByDescending(b => b.addDate).Take(3);
        }
    }
}
