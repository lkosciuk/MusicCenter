using MusicCenter.Common.ViewModels.Band;
using MusicCenter.Dal.EntityModels;
using MusicCenter.Dal.Repositories;
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
using Repository.Pattern.Infrastructure;
using System.Text.RegularExpressions;
using System.Globalization;

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


        public bool IfBandExists(string name)
        {
            return _repo.Queryable().Any(b => b.name.ToLower() == name.ToLower());
        }

        public void AddBand(AddBandViewModel model)
        {
            Users currentUser = _unitOfWork.Repository<Users>().Queryable().FirstOrDefault(u => u.email == model.UserEmail);

            Band newBand = new Band()
            {
                name = model.Name,
                email = model.Email,
                phoneNumber = model.Phone,
                addDate = DateTime.Now,
                bandCreationDate = String.IsNullOrEmpty(model.CreationDate) ? null : (DateTime?)DateTime.ParseExact(model.CreationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                bandResolveDate = String.IsNullOrEmpty(model.ResolveDate) ? null : (DateTime?)DateTime.ParseExact(model.ResolveDate, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                ObjectState = ObjectState.Added,
                user = currentUser,
                UserID = currentUser.Id,
                description = model.Description
            };

            currentUser.bands.Add(newBand);

            List<Genre> BandGenres = new List<Genre>();
            List<BandMember> Members = new List<BandMember>();
            Files bandAvatar;

            if (!String.IsNullOrEmpty(model.Genres))
            {
                Regex.Replace(model.Genres, @"\s+", "");
                string[] Genres = model.Genres.Split(',');


                foreach (var genre in Genres)
                {
                    if (!_unitOfWork.Repository<Genre>().IsGenreExists(genre))
                    {
                        Genre newGenre = new Genre() { name = genre, ObjectState = ObjectState.Added };
                        newGenre.bands.Add(newBand);
                        BandGenres.Add(newGenre);
                    }
                    else
                    {
                        Genre existingGenre = _unitOfWork.Repository<Genre>().GetGenreByName(genre);
                        existingGenre.bands.Add(newBand);
                        BandGenres.Add(existingGenre);
                    }


                }
            }

            if (model.BandMembers != null)
            {
                foreach (var member in model.BandMembers)
                {
                    BandMember newMember = new BandMember() { fullName = member, ObjectState = ObjectState.Added };
                    newMember.bands.Add(newBand);
                    Members.Add(newMember);
                }
            }           

            if (model.Avatar.PostedFile != null)
            {
                bandAvatar = new Files();
                bandAvatar.band = newBand;
                bandAvatar.IsAvatar = true;
                bandAvatar.ObjectState = ObjectState.Added;
                bandAvatar.name = model.Avatar.PostedFile.FileName;
                bandAvatar.path = "/Content/Uploads/" + model.Avatar.PostedFile.FileName;
                model.Avatar.PostedFile.SaveAs(model.Avatar.RelativePathToSave);
            }
            else
            {
                bandAvatar = new Files();
                bandAvatar.band = newBand;
                bandAvatar.IsAvatar = true;
                bandAvatar.ObjectState = ObjectState.Added;
                bandAvatar.name = "DefaultBandAv.png";
                bandAvatar.path = "/Content/Uploads/DefaultBandAv.png";
            }

            newBand.genres = BandGenres;
            newBand.members = Members;
            newBand.images.Add(bandAvatar);

            _repo.InsertOrUpdateGraph(newBand);
            _unitOfWork.SaveChanges();
        }


        public BandPanelViewModel GetBandPanelViewModelByName(string name)
        {
            return _repo.Queryable().Where(b => b.name == name)
                    .Select(band => new BandPanelViewModel()
                    {
                        Name = band.name,
                        AvatarPath = band.images.FirstOrDefault(i => i.IsAvatar).path,
                        MessagesCount = band.receivedMessages.Where(m => !m.isReaded).Count()
                    }).FirstOrDefault();
        }

        public BandProfileViewModel GetBandProfileViewModel(string BandName)
        {
            Band currentBand = _repo.GetBandByName(BandName, b => b.images, b => b.genres, b => b.members).FirstOrDefault();

            return  new BandProfileViewModel()
                   {
                       Avatar = new FileViewModel() { PathToShow = currentBand.images.FirstOrDefault(i => i.IsAvatar).path },
                       BandMembers = currentBand.members.Select(m => m.fullName).ToArray(),
                       CreationDate = currentBand.bandCreationDate.HasValue ? currentBand.bandCreationDate.Value.ToShortDateString() : null ,
                       ResolveDate = currentBand.bandResolveDate.HasValue ? currentBand.bandResolveDate.Value.ToShortDateString() : null,
                       Description = currentBand.description,
                       Email = currentBand.email,
                       Genres = String.Join(",", currentBand.genres.Select(g => g.name).ToArray()),
                       Name = currentBand.name,
                       Phone = currentBand.phoneNumber
                   };
        }
    }
}
