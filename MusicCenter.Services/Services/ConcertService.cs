using MusicCenter.Common.ViewModels.Concert;
using MusicCenter.Dal.EntityModels;
using MusicCenter.Services.Intefaces;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Dal.Repositories;
using MusicCenter.Common.ViewModels.File;
using System.Data.Entity;
using System.Linq;

namespace MusicCenter.Services.Services
{
    public class ConcertService : BaseService<Concert>, IConcertService
    {
         public ConcertService(IUnitOfWork u)
            : base(u)
        {
            
        }

         public BandConcertListViewModel GetBandConcertListViewModel(string BandName)
         {
             var BandConcerts = _repo.GetBandConcerts(BandName, c => c.bands, c => c.favourites, c => c.images).ToList();

             return new BandConcertListViewModel(){
                 BandName = BandName,
                 Concerts = BandConcerts.Select(c => new ConcertListItem(){
                     address = c.address,
                     date = c.date,
                     description = c.description,
                     InterestedCount = c.favourites.Count,
                     Cover = new FileViewModel(){PathToShow = c.images.FirstOrDefault(i => i.IsAvatar).path}
                 }).ToList()
             };
             
         }
    }
}
