using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class AddBandViewModel
    {
        public string UserEmail { get; set; }

        public FileViewModel Avatar { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        [EmailAddress(ErrorMessageResourceName = "EmailValid", ErrorMessageResourceType = typeof(Resources.Global))]
        public string Email { get; set; }

        public string Phone { get; set; }
        
        public string CreationDate { get; set; }

        public string ResolveDate { get; set; }

        public string Genres { get; set; }

        public string[] BandMembers { get; set; }

        public string Description { get; set; }


    }
}
