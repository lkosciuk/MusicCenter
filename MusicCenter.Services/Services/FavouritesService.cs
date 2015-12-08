using MusicCenter.Dal.EntityModels;
using MusicCenter.Services.Intefaces;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Services
{
    public class FavouritesService : BaseService<Favourites>, IFavouritesService
    {
        public FavouritesService(IUnitOfWork u)
            : base(u)
        {
            
        }
    }
}
