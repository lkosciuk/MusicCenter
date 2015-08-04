using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.RepoExt
{
    public static class RepositoryExt
    {
        public static Entity GetById<Entity>(this IRepository<Entity> repository, int id)
            where Entity : BaseEntity
        {
            return repository
                .Queryable()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
