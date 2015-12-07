using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Dal.EntityModels;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Dal.RepoExt;
using System.Data.Entity;
using MusicCenter.Common.ViewModels.File;
using Repository.Pattern.UnitOfWork;

namespace MusicCenter.Services.Services
{
    public class BandService : BaseService<Band>, IBandService
    {
        public BandService(IUnitOfWork u)
            : base(u)
        {
            
        }
        public List<BandListItemViewModel> GetUserBandList(string email)
        {
            var bands = _repo.Queryable()
                .Include(b => b.genres)
                .Include(b => b.images)
                .Where(b => b.user.email == email)
                .Select(band => new BandListItemViewModel() 
                { 
                  Name = band.name,
                  Genres = band.genres.Select(genres => genres.name).ToList(),
                  CreationDate = band.bandCreationDate,
                  ResolveDate = band.bandResolveDate,
                  Description = band.description,
                  Avatar = new FileViewModel()
                            {
                                PathToShow = band.images.Where(i => i.IsAvatar).FirstOrDefault().path                                                             
                            }
                });

            return bands.ToList();
        }
    }
}
