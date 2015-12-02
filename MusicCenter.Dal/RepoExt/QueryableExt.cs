using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace MusicCenter.Common.Extensions
{
    public static class QueryableExt
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> entities, Expression<Func<T, object>>[] includes)
        {
            foreach (var item in includes)
            {
                entities = entities.Include(item);
            }

            return entities;
        }
    }
}
