using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MusicCenter.Common.ViewModels.User
{
    public class UserProfileViewModel
    {
        public string email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources.Global))]
        [MinLength(5, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(Resources.Global))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources.Global))]
        [MinLength(5, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(Resources.Global))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "PasswordConfirmRequired", ErrorMessageResourceType = typeof(Resources.Global))]
        [Compare("Password", ErrorMessageResourceName = "PasswordConfirmMatch", ErrorMessageResourceType = typeof(Resources.Global))]
        public string PasswordConfirm { get; set; }

        public FileViewModel Avatar { get; set; }
    }
}
