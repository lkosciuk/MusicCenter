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
                userAvatar.path = "~/Content/Uploads/" + RegisterModel.Avatar.FileName;
                RegisterModel.Avatar.SaveAs(RegisterModel.AvatarRelativePath); 
            }
            else
            {
                userAvatar = new Files();
                userAvatar.name = "DefaultUserAv.jpg";
                userAvatar.path = "~/Content/Uploads/DefaultUserAv.jpg";
            }

            _unitOfWork.BeginTransaction();

            userAvatar.ObjectState = ObjectState.Added;
            userAvatar.user = newUser;
            _unitOfWork.Repository<Files>().Insert(userAvatar);
            _unitOfWork.SaveChanges();

            newUser.profilePhoto = userAvatar;

            Role userRole = _unitOfWork.Repository<Role>().GetRoleByName("user");
            userRole.ObjectState = ObjectState.Unchanged;
            newUser.roles.Add(userRole);

            Favourites favourites = new Favourites();
            favourites.ObjectState = ObjectState.Added;
            _unitOfWork.Repository<Favourites>().Insert(favourites);
            _unitOfWork.SaveChanges();

            newUser.favourites = favourites;
            newUser.ObjectState = ObjectState.Added;
            _repo.Insert(newUser);
            _unitOfWork.SaveChanges();

            userAvatar.user = newUser;
            userAvatar.ObjectState = ObjectState.Modified;
            favourites.user = newUser;
            favourites.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<Files>().Update(userAvatar);
            _unitOfWork.Repository<Favourites>().Update(favourites);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

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
    }
}
