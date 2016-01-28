using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Concert
{
    public class ConcertViewModel
    {
        public string BandName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime date { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public FileViewModel Image { get; set; }
        public List<BandConcertViewModel> Bands { get; set; }
        public int InterestedCount { get; set; }
    }
}
