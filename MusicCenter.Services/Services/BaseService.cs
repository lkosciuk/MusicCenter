using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using MusicCenter.Dal.EntityModels;
using Repository.Pattern;
using Repository.Pattern.UnitOfWork;

namespace MusicCenter.Services.Services
{
    public abstract class BaseService<Entity>
        where Entity : BaseEntity
    {
        protected readonly IRepository<Entity> _repo;

        public BaseService(IUnitOfWork u)
        {
            _repo = u.Repository<Entity>();
        }

        public bool IsExists(int id)
        {
            return _repo.Queryable().Any(x => x.Id == id);
        }
    }
}
