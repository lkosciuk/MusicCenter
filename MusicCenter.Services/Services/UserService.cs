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
            string avatarPath = "~/Content/Uploads";

            Files avatar = new Files();
            Users newUser = new Users();
            Role role = new Role();

            if (RegisterModel.Avatar.ContentLength > 0)
            {
                avatar = new Files()
                {
                    name = RegisterModel.Avatar.FileName,
                    path = avatarPath,
                    ObjectState = ObjectState.Added
                };
                
                newUser = new Users()
                {
                    ObjectState = ObjectState.Added,
                    email = RegisterModel.Email,
                    password = RegisterModel.Password,
                    profilePhoto = avatar
                    //roles = new Role() { }
                };
            }

            


            //newUser = Mapper.Map<RegisterViewModel, Users>(urvm);
            //newUser.login = urvm.login;
            //newUser.password = urvm.password;
            //newUser.email = urvm.email;
            //...

            //TODO: automaper

            _repo.Insert(newUser);
        }
    }
}
