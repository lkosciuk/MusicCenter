using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Concert
{
    public class UpdateConcertViewModel
    {
        public string BandName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        public string date { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        public string address { get; set; }

        public string description { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public FileViewModel Cover { get; set; }

        public List<FileViewModel> Images { get; set; }

        public List<BandConcertViewModel> Bands { get; set; }
    }
}
