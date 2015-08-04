using AutoMapper;
using MusicCenter.Common.ViewModels;
using MusicCenter.Dal.EntityModels;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Services
{
    public class UsersService
         : BaseService<Users>
    {


        public UsersService(IUnitOfWork u)
            : base(u)
        {

        }

        public void Register(UserRegisterViewModel urvm)
        {
            Users newUser = new Users() { ObjectState = ObjectState.Added };
            newUser = Mapper.Map<UserRegisterViewModel, Users >(urvm);
            //newUser.login = urvm.login;
            //newUser.password = urvm.password;
            //newUser.email = urvm.email;
            //...

            //TODO: automaper

            _repo.Insert(newUser);
        }
    }
}
