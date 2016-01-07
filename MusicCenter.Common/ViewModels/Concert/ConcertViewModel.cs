using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Concert
{
    public class ConcertViewModel
    {
        public string BandName { get; set; }
        public DateTime date { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public float coordinatesX { get; set; }
        public float coordinatesY { get; set; }
        public List<FileViewModel> Images { get; set; }
        public List<BandConcertViewModel> Bands { get; set; }
        public int InterestedCount { get; set; }
    }
}
