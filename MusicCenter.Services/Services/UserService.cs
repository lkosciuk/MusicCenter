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
        //private IRoleService RoleService;
        //private IFilesService FilesService;

        public UsersService(IUnitOfWork u, IRoleService roleService, IFilesService fileService)
            : base(u)
        {
            //RoleService = roleService;
            //FilesService = fileService;
            
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

            newUser.email = RegisterModel.Email;
            newUser.password = RegisterModel.Password;

            if (RegisterModel.Avatar != null)
            {
                Files addedFile = new Files();
                addedFile.name = RegisterModel.Avatar.FileName;
                addedFile.path = "~/Content/Uploads/" + RegisterModel.Avatar.FileName;

                addedFile.ObjectState = ObjectState.Added;
                _unitOfWork.Repository<Files>().Insert(addedFile);
                _unitOfWork.SaveChanges();

                RegisterModel.Avatar.SaveAs(RegisterModel.AvatarRelativePath);
                newUser.profilePhoto = addedFile;
            }

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
            
        }
    }
}
