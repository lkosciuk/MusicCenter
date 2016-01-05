using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class UpdateAlbumViewModel
    {
        public string BandName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ReleaseDate { get; set; }

        public string Label { get; set; }

        public string Producer { get; set; }

        public string Genres { get; set; }

        public FileViewModel Cover { get; set; }

        public string[] SongsToRemove { get; set; }

        public string[] NewSongsNames { get; set; }

        public string[] NewSongsUrlAddresses { get; set; }

        public List<BandSingleViewModel> ExistingSongs { get; set; }
    }
}
