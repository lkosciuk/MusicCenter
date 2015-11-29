using MusicCenter.Common.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Intefaces
{
    public interface IUserService
    {
        bool IfUserExists(string login);
        void Register(RegisterViewModel urvm);

        UserPanelViewModel GerUserPanelViewModelByEmail(string email);

        void SoundCloudRegister(SoundCloudRegisterViewModel userData);

        bool VerifyLoginAndPassword(LoginViewModel model);
    }
}
