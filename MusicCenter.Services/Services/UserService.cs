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

            if (RegisterModel.Avatar != null)
            {
                userAvatar = new Files();
                userAvatar.name = RegisterModel.Avatar.FileName;
                userAvatar.path = "/Content/Uploads/" + RegisterModel.Avatar.FileName;
                RegisterModel.Avatar.SaveAs(RegisterModel.AvatarRelativePath); 
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
            Users loggedUser = _repo.GetUserByEmail(email);

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

        public bool VerifyLoginAndPassword(LoginViewModel model)
        {
            return _repo.Queryable().Any(u => u.email == model.Email && u.password == model.Password);
        }
    }
}
