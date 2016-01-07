using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class AddSingleViewModel
    {
        public string BandName { get; set; }

        public string SongName { get; set; }

        public string SongUrl { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        public string ReleaseDate { get; set; }

        public string Genres { get; set; }
    }
}
