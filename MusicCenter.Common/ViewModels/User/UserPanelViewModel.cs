using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.User
{
    public class UserPanelViewModel
    {
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public int MessagesCount { get; set; }
    }
}
