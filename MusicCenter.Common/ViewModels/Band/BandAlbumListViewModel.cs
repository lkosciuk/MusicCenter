using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class BandAlbumListViewModel
    {
        public List<BandAlbumViewModel> Albums { get; set; }

        public string BandName { get; set; }
    }
}
