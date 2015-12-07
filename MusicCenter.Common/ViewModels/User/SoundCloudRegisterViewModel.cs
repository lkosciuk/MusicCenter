using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.User
{
    public class SoundCloudRegisterViewModel
    {
        public string username { get; set; }
        public string avatar_url { get; set; }
        public string full_name { get; set; }

        public string FirstName
        {
            get
            {
                if (!String.IsNullOrEmpty(full_name))
                {
                    string[] words = full_name.Split(' ');

                    if (words.Count() > 1)
                    {
                        return words[0];
                    }
                }

                return "";
                
            }
        }

        public string LastName
        {
            get
            {
                if (!String.IsNullOrEmpty(full_name))
                {
                    string[] words = full_name.Split(' ');

                    if (words.Count() > 1)
                    {
                        return words[1];
                    }
                }

                return "";
            }
        }
    }
}
