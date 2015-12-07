﻿using MusicCenter.Common.ViewModels.Message;
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

        bool VerifyLoginAndPassword(string email, string password);
        UserProfileViewModel GetUserProfile(string email);

        void UpdateUser(UserProfileViewModel model);

        UserSoundcloudProfileViewModel GetUserSoundcloudProfile(string email);

        void UpdateSoundCloudUser(UserSoundcloudProfileViewModel model);

        List<MessageLisItemViewModel> GetUserReceivedMessages(string email);
    }
}
