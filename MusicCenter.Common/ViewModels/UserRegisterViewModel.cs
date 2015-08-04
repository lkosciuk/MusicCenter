using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicCenter.Common.Extensions;

namespace MusicCenter.Common.ViewModels
{
    public class UserRegisterViewModel
    {
        public string login { get; set; }
        public string password { get; set; }
        public string passwordRep { get; set; }
        public string email { get; set; }

        public UserRegisterViewModel()
        {
            
        }
    }
}
