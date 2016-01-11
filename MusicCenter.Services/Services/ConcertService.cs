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
using MusicCenter.Dal.RepoExt;

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


         public BandConcertViewModel GetBandConcertViewModel(string BandName)
         {
             Band currentBand = _unitOfWork.Repository<Band>().GetBandByName(BandName, b => b.genres, b => b.images).FirstOrDefault();

             return new BandConcertViewModel()
             {
                 Avatar = new FileViewModel() { PathToShow = currentBand.images.FirstOrDefault(i => i.IsAvatar).path },
                 BandName = currentBand.name,
                 CreationDate = currentBand.bandCreationDate,
                 Description = currentBand.description,
                 Genres = String.Join(",", currentBand.genres.Select(g => g.name).ToArray())
             };
         }


         public UpdateConcertViewModel GetUpdateConcertViewModel(int ConcertId)
         {
             Concert currentConcert = _repo.GetById(ConcertId);
             List<BandConcertViewModel> concertBands; //trzeba wyciagnac ta liste juz tutqaj zeby miec dostep do gatunków

             return new UpdateConcertViewModel()
             {
                 address = currentConcert.address,
                 Bands = new List<BandConcertViewModel>(currentConcert.bands.Select(b => new BandConcertViewModel() {
                     Avatar = b.images.Where(i => i.IsAvatar).Select(i => new FileViewModel() { FileId = i.Id, PathToShow = i.path}).FirstOrDefault(),
                     BandName = b.name,
                     CreationDate = b.bandCreationDate,
                     Description = b.description,
                     Genres = String.Join(",", b.genres.Select(g => g.name).ToArray())//to nie przejdzie, nie ma includa do genres i obiekt dalej jest queryable wiec string.join nie zadziala
                 })),
                 coordinatesX = currentConcert.coordinatesX,
                 coordinatesY = currentConcert.coordinatesY,
                 Cover = new FileViewModel() { PathToShow = currentConcert.images.FirstOrDefault(i => i.IsAvatar).path },
                 date = currentConcert.date.ToShortDateString(),
                 description = currentConcert.description,
                 Images = new List<FileViewModel>(
                     currentConcert.images.Where(i => !i.IsAvatar).Select(i => new FileViewModel()
                     {
                         FileId = i.Id,
                         PathToShow = i.path
                     }))
             };
         }
    }
}
