using AutoMapper;
using MusicCenter.Common.ViewModels;
using MusicCenter.Common.ViewModels.User;
using MusicCenter.Dal.EntityModels;
using MusicCenter.Services.Intefaces;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Dal.Repositories;
using System.IO;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web;
using MusicCenter.Common.ViewModels.File;
using MusicCenter.Common.ViewModels.Message;
using MusicCenter.Dal.Repositories;
using System.Text.RegularExpressions;

namespace MusicCenter.Services.Services
{
    public class UsersService
         : BaseService<Users>, IUserService
    {

        public UsersService(IUnitOfWork u)
            : base(u)
        {
            
        }

        public bool IfUserExists(string email)
        {
            if (_repo.IsUserExists(email))
            {
                return true;
            }

            return false;
        }

        public void Register(RegisterViewModel RegisterModel)
        {
            Users newUser = new Users();
            Files userAvatar = new Files();

            newUser.email = RegisterModel.Email;
            newUser.password = RegisterModel.Password;

            if (RegisterModel.Avatar.PostedFile != null)
            {
                userAvatar = new Files();
                userAvatar.name = RegisterModel.Avatar.PostedFile.FileName;
                userAvatar.path = "/Content/Uploads/" + RegisterModel.Avatar.PostedFile.FileName;
                RegisterModel.Avatar.PostedFile.SaveAs(RegisterModel.Avatar.RelativePathToSave); 
            }
            else
            {
                userAvatar = new Files();
                userAvatar.name = "DefaultUserAv.jpg";
                userAvatar.path = "/Content/Uploads/DefaultUserAv.jpg";
            }

            _unitOfWork.BeginTransaction();

            userAvatar.ObjectState = ObjectState.Added;
            userAvatar.user = newUser;

            newUser.profilePhoto = userAvatar;

            Role userRole = _unitOfWork.Repository<Role>().GetRoleByName("user");
            userRole.ObjectState = ObjectState.Unchanged;
            newUser.roles.Add(userRole);
            userRole.Users.Add(newUser);

            Favourites favourites = new Favourites();
            favourites.ObjectState = ObjectState.Added;

            newUser.favourites = favourites;
            favourites.user = newUser;

            newUser.ObjectState = ObjectState.Added;

            _repo.InsertOrUpdateGraph(newUser);
            try
            {
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            
        }


        public UserPanelViewModel GerUserPanelViewModelByEmail(string email)
        {
            Users loggedUser = _repo.GetUserByEmail(email, u => u.favourites, u => u.profilePhoto, u => u.roles, u => u.receivedMessages);

            UserPanelViewModel model = new UserPanelViewModel()
            {
                Email = loggedUser.email,
                AvatarPath = loggedUser.profilePhoto.path,
                MessagesCount = loggedUser.receivedMessages.Where(m => m.isReaded == false).Count()
            };

            return model;
        }


        public void SoundCloudRegister(SoundCloudRegisterViewModel userData)
        {
            Users newUser = new Users();
            Files userAvatar = new Files();

            newUser.email = userData.username;
            newUser.firstName = userData.FirstName;
            newUser.lastName = userData.LastName;

            userAvatar.path = userData.avatar_url;
            userAvatar.name = userData.username + " " + "SoundCloud";
            
            _unitOfWork.BeginTransaction();

            userAvatar.ObjectState = ObjectState.Added;
            userAvatar.user = newUser;

            newUser.profilePhoto = userAvatar;

            Role userRole = _unitOfWork.Repository<Role>().GetRoleByName("user");
            userRole.ObjectState = ObjectState.Unchanged;
            newUser.roles.Add(userRole);
            userRole.Users.Add(newUser);

            Favourites favourites = new Favourites();
            favourites.ObjectState = ObjectState.Added;

            newUser.favourites = favourites;
            favourites.user = newUser;

            newUser.ObjectState = ObjectState.Added;

            _repo.InsertOrUpdateGraph(newUser);
            try
            {
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        public bool VerifyLoginAndPassword(string email, string password)
        {
            return _repo.Queryable().Any(u => u.email == email && u.password == password);
        }

        public UserProfileViewModel GetUserProfile(string email)
        {
            Users currentUser = _repo.GetUserByEmail(email, u => u.profilePhoto);

            return new UserProfileViewModel()
                       {
                            FirstName = currentUser.firstName,
                            LastName = currentUser.lastName,
                            Avatar = new FileViewModel() { PathToShow = currentUser.profilePhoto.path}
                       };
        }


        public void UpdateUser(UserProfileViewModel model)
        {
            Users currentUser = _repo.GetUserByEmail(model.email, u => u.profilePhoto);
            currentUser.firstName = model.FirstName;
            currentUser.lastName = model.LastName;
            currentUser.password = model.Password;

            if (model.Avatar.PostedFile != null)
            {
                currentUser.profilePhoto.ObjectState = ObjectState.Modified;
                currentUser.profilePhoto.name = model.Avatar.PostedFile.FileName;
                currentUser.profilePhoto.path = "/Content/Uploads/" + model.Avatar.PostedFile.FileName;
                model.Avatar.PostedFile.SaveAs(model.Avatar.RelativePathToSave);
            }
            currentUser.ObjectState = ObjectState.Modified;

            _repo.InsertOrUpdateGraph(currentUser);
            _unitOfWork.SaveChanges();
        }


        public UserSoundcloudProfileViewModel GetUserSoundcloudProfile(string email)
        {
            Users currentUser = _repo.GetUserByEmail(email, u => u.profilePhoto);

            return new UserSoundcloudProfileViewModel()
            {
                FirstName = currentUser.firstName,
                LastName = currentUser.lastName,
                Avatar = new FileViewModel() { PathToShow = currentUser.profilePhoto.path }
            };
        }


        public void UpdateSoundCloudUser(UserSoundcloudProfileViewModel model)
        {
            Users currentUser = _repo.GetUserByEmail(model.email, u => u.profilePhoto);
            currentUser.firstName = model.FirstName;
            currentUser.lastName = model.LastName;

            if (model.Avatar.PostedFile != null)
            {
                currentUser.profilePhoto.ObjectState = ObjectState.Modified;
                currentUser.profilePhoto.name = model.Avatar.PostedFile.FileName;
                currentUser.profilePhoto.path = "/Content/Uploads/" + model.Avatar.PostedFile.FileName;
                model.Avatar.PostedFile.SaveAs(model.Avatar.RelativePathToSave);
            }
            currentUser.ObjectState = ObjectState.Modified;

            _repo.InsertOrUpdateGraph(currentUser);
            _unitOfWork.SaveChanges();
        }
    }
}
