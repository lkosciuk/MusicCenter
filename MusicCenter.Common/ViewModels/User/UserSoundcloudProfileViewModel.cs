using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.User
{
    public class UserSoundcloudProfileViewModel
    {
        public string email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public FileViewModel Avatar { get; set; }
    }
}
