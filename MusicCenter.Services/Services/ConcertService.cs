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
using MusicCenter.Dal;
using MusicCenter.Dal.RepoExt;
using Repository.Pattern.Infrastructure;
using MusicCenter.Common.ViewModels.Common;
using MusicCenter.Common.Enums;
using MusicCenter.Common.Extensions;
using MusicCenter.Common.RequestModels;
using Webdiyer.WebControls.Mvc;
using MusicCenter.Common.Helpers;

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
             var BandConcerts = _repo.GetBandConcerts(BandName, c => c.bands, c => c.favourites, c => c.images, c => c.ConcertOwner).ToList();

             return new BandConcertListViewModel(){
                 BandName = BandName,
                 Concerts = BandConcerts.Select(c => new ConcertListItem(){
                     Id = c.Id,
                     address = c.address,
                     date = c.date,
                     description = c.description,
                     InterestedCount = c.favourites.Count,
                     Cover = new FileViewModel(){PathToShow = c.images.FirstOrDefault(i => i.IsAvatar).path},
                     ConcertOwner = c.ConcertOwner.name
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
             Concert currentConcert = _repo.GetById(ConcertId, c => c.bands, c => c.ConcertOwner, c => c.images );
             List<string> concertBands = new List<string>();

             foreach (var item in currentConcert.bands)
	         {
                 concertBands.Add(item.name);   		 
	         }

             return new UpdateConcertViewModel()
             {
                 ConcertId = currentConcert.Id,
                 BandName = currentConcert.ConcertOwner.name,
                 address = currentConcert.address,
                 Bands = concertBands,
                 Latitude = currentConcert.Latitude,
                 Longitude = currentConcert.Longitude,
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


         public void AddConcert(AddConcertViewModel model)
         {
             Band concertOwner = _unitOfWork.Repository<Band>().GetBandByName(model.BandName).FirstOrDefault();

             Concert newConcert = new Concert()
             {
                 address = model.address,
                 date = DateTime.Parse(model.date),
                 Latitude = model.Latitude,
                 Longitude = model.Longitude,
                 description = model.description,
                 ObjectState = ObjectState.Added,
                 ConcertOwner = concertOwner
             };

             concertOwner.OwnedConcerts.Add(newConcert);

             Files concertAvatar;

             if (model.Cover.PostedFile != null)
             {
                 concertAvatar = new Files();
                 concertAvatar.concert = newConcert;
                 concertAvatar.IsAvatar = true;
                 concertAvatar.ObjectState = ObjectState.Added;
                 concertAvatar.name = model.Cover.PostedFile.FileName;
                 concertAvatar.path = "/Content/Uploads/" + model.Cover.PostedFile.FileName;
                 model.Cover.PostedFile.SaveAs(model.Cover.RelativePathToSave);
             }
             else
             {
                 concertAvatar = new Files();
                 concertAvatar.concert = newConcert;
                 concertAvatar.IsAvatar = true;
                 concertAvatar.ObjectState = ObjectState.Added;
                 concertAvatar.name = "DefaultConcertAv.jpg";
                 concertAvatar.path = "/Content/Uploads/DefaultConcertAv.jpg";
             }

            if (model.Bands != null)
            {
                foreach (var bandName in model.Bands)
                {
                    Band concertBand = _unitOfWork.Repository<Band>().GetBandByName(bandName).FirstOrDefault();
                    newConcert.bands.Add(concertBand);
                    concertBand.MemberConcerts.Add(newConcert);
                }
            }
             
             newConcert.images.Add(concertAvatar);

             _repo.InsertOrUpdateGraph(newConcert);
             _unitOfWork.SaveChanges();

         }


         public ConcertViewModel GetConcertViewModel(int ConcertId)
         {
             Concert currentConcert = _repo.GetById(ConcertId, c => c.bands, c => c.ConcertOwner, c => c.images, c => c.favourites);
             List<Band> concertBands = _unitOfWork.Repository<Band>().GetBandsByConcert(ConcertId, b => b.images, b => b.genres).ToList();

             return new ConcertViewModel()
             {
                 ConcertId = currentConcert.Id,
                 BandName = currentConcert.ConcertOwner.name,
                 address = currentConcert.address,
                 date = currentConcert.date,
                 description = currentConcert.description,
                 Latitude = currentConcert.Latitude,
                 Longitude = currentConcert.Longitude,
                 InterestedCount = currentConcert.favourites.Count,
                 Image = new FileViewModel() { PathToShow = currentConcert.images.FirstOrDefault(i => i.IsAvatar).path },
                 Bands = new List<BandConcertViewModel>(concertBands.Select(b => new BandConcertViewModel() {
                     Avatar = new FileViewModel() { PathToShow = b.images.FirstOrDefault(i => i.IsAvatar).path },
                     BandName = b.name,
                     CreationDate = b.bandCreationDate,
                     Description = b.description,
                     Genres = String.Join(",", b.genres.Select(g => g.name).ToArray())
                 }))
             };
         }


         public bool IsVisitorConcertOwner(string BandName, int ConcertId)
         {
                return _repo.GetById(ConcertId, c => c.ConcertOwner).ConcertOwner.name == BandName;
         }

         public void DeleteConcert(int ConcertId)
         {
             Concert concertToRemove = _repo.GetById(ConcertId, c => c.images);
             concertToRemove.ObjectState = ObjectState.Deleted;

             foreach (var item in concertToRemove.images)
             {
                 item.ObjectState = ObjectState.Deleted;
             }

             _repo.InsertOrUpdateGraph(concertToRemove);
             _repo.Delete(concertToRemove);
             _unitOfWork.SaveChanges();
         }


         public void UpdateConcert(UpdateConcertViewModel model)
         {
             Concert currentConcert = _repo.GetById(model.ConcertId, c => c.bands, c => c.images);

             currentConcert.ObjectState = ObjectState.Modified;
             currentConcert.date = DateTime.Parse(model.date);
             currentConcert.address = model.address;
             currentConcert.Latitude = model.Latitude;
             currentConcert.Longitude = model.Longitude;
             currentConcert.description = model.description;

             Files concertAvatar;

             if (model.Cover.PostedFile != null)
             {
                 Files oldAvatar = currentConcert.images.FirstOrDefault(i => i.IsAvatar);
                 oldAvatar.ObjectState = ObjectState.Modified;
                 oldAvatar.IsAvatar = false;

                 concertAvatar = new Files();
                 concertAvatar.concert = currentConcert;
                 concertAvatar.IsAvatar = true;
                 concertAvatar.ObjectState = ObjectState.Added;
                 concertAvatar.name = model.Cover.PostedFile.FileName;
                 concertAvatar.path = "/Content/Uploads/" + model.Cover.PostedFile.FileName;
                 model.Cover.PostedFile.SaveAs(model.Cover.RelativePathToSave);
                 currentConcert.images.Add(concertAvatar);
             }
      
             foreach (var band in currentConcert.bands)
             {
                 band.MemberConcerts.Remove(currentConcert);
                 band.ObjectState = ObjectState.Modified;
             }

             currentConcert.bands = new List<Band>();

            if (model.Bands != null)
            {
                foreach (var band in model.Bands)
                {
                    Band concertBand = _unitOfWork.Repository<Band>().GetBandByName(band).FirstOrDefault();
                    currentConcert.bands.Add(concertBand);
                    concertBand.MemberConcerts.Add(currentConcert);
                }
            }

             _repo.InsertOrUpdateGraph(currentConcert);
             _unitOfWork.SaveChanges();
             
         }


         public List<ConcertViewModel> GetConcertsInMonth(int year, int month)
         {
             List<ConcertViewModel> concertsInMonth = new List<ConcertViewModel>();

             var selectedConcerts = _repo.GetAllConcertsInMonth(year, month);

             foreach (var item in selectedConcerts)
	         {
                 concertsInMonth.Add(GetConcertViewModel(item.Id));
	         }

             return concertsInMonth;          
         }


         public IEnumerable<SearchViewModel> SearchConcerts(string query)
         {
             var concerts = _repo.Queryable().Include(c => c.ConcertOwner).Include(c => c.bands).Where(b => b.bands.Any(c => c.name.IndexOf(query) != -1)
                                 || b.address.IndexOf(query) != -1 || b.ConcertOwner.name.IndexOf(query) != -1).ToList();

             var result = new List<SearchViewModel>(concerts.Select(b => new SearchViewModel()
                                                   {
                                                       label = b.ConcertOwner.name + ", " + String.Join(",", b.bands.Select(g => g.name).ToArray()),
                                                       value = b.Id.ToString()
                                                   }).ToList());

             foreach (var item in result)
             {
                 item.category = SearchCategory.Concerts.ShowResourcesString();
             }

             return result;
         }

        public PagedList<ConcertListItemViewModel> GetConcertListByPageNuber(DataListFilterModel filter, int pageNumber)
        {
            if (filter != null && filter.HasValue)
            {
                var concerts = from concertQuery in _repo.Queryable()
                               let bands = concertQuery.bands
                               let genres = bands.SelectMany(b => b.genres)
                               let file = concertQuery.images.FirstOrDefault(i => i.IsAvatar)
                               orderby concertQuery.date descending
                               select new ConcertListItemViewModel()
                               {
                                   Id = concertQuery.Id,
                                   ConcertOwner = concertQuery.ConcertOwner.name,
                                   Image = new FileViewModel() { PathToShow = file.path },
                                   BandNames = bands.Select(b => b.name).Concat(new string[] { concertQuery.ConcertOwner.name }).ToList(),
                                   Date = concertQuery.date,
                                   Genres = genres.Select(a => a.name).Concat(concertQuery.ConcertOwner.genres.Select(g => g.name)).ToList(),
                                   Address = concertQuery.address
                               };

                if (filter.DateFrom.HasValue)
                {
                    concerts = concerts.Where(b => b.Date >= filter.DateFrom.Value);
                }

                if (filter.DateTo.HasValue)
                {
                    concerts = concerts.Where(b => b.Date <= filter.DateTo.Value);
                }

                if (!string.IsNullOrEmpty(filter.Names))
                {
                    var bandNames = filter.Names.Split(',');
                    bandNames = bandNames.Select(g => g.Trim()).ToArray();

                    concerts = concerts.Where(b => bandNames.Any(filterBand => b.BandNames.Any(band => band == filterBand)));
                }

                if (!string.IsNullOrEmpty(filter.GenreNames))
                {
                    var genreNames = filter.GenreNames.Split(',');
                    genreNames = genreNames.Select(g => g.Trim()).ToArray();

                    concerts = concerts.Where(b => genreNames.Any(filterGenre => b.Genres.Any(genre => genre == filterGenre)));
                }

                return concerts.ToPagedList(pageNumber, ConstHelper.GridPageSize);
            }
            else
            {
                var concerts = from concertQuery in _repo.Queryable()
                               let bands = concertQuery.bands
                               let genres = bands.SelectMany(b => b.genres)
                               let file = concertQuery.images.FirstOrDefault(i => i.IsAvatar)
                               orderby concertQuery.date descending
                               select new ConcertListItemViewModel()
                               {
                                   Id = concertQuery.Id,
                                   ConcertOwner = concertQuery.ConcertOwner.name,
                                   Image = new FileViewModel() { PathToShow = file.path },
                                   BandNames = bands.Select(b => b.name).Concat(new string[] { concertQuery.ConcertOwner.name }).ToList(),
                                   Date = concertQuery.date,
                                   Genres = genres.Select(a => a.name).Concat(concertQuery.ConcertOwner.genres.Select(g => g.name)).ToList(),
                                   Address = concertQuery.address
                               };

                return concerts.ToPagedList(pageNumber, ConstHelper.GridPageSize);
            }
        }
    }
}
