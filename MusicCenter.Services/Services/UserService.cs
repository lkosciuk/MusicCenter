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
        private IRoleService RoleService;
        private IFilesService FilesService;

        public UsersService(IUnitOfWork u, IRoleService roleService, IFilesService fileService)
            : base(u)
        {
            RoleService = roleService;
            FilesService = fileService;
            
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

            if (RegisterModel.Avatar.ContentLength > 0)
            {
                Files addedFile = FilesService.AddFile(RegisterModel.Avatar.FileName, "~/Content/Uploads/" + RegisterModel.Avatar.FileName);
                RegisterModel.Avatar.SaveAs(RegisterModel.AvatarRelativePath);
                newUser.profilePhoto = addedFile;
            }

            newUser.roles.Add(RoleService.GetRoleByName("user"));

            _repo.Insert(newUser);
            _unitOfWork.SaveChanges();
        }
    }
}
