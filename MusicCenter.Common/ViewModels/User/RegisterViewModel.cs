using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MusicCenter.Common.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="Email is required")]
        [EmailAddress(ErrorMessage="It's not valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage="Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage="You need to confirm password")]
        [Compare("Password", ErrorMessage="Password not match")]
        public string PasswordConfirm { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Avatar { get; set; }
    }
}
