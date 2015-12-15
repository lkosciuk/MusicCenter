using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class BandSingleListViewModel
    {
        public string BandName { get; set; }

        public List<BandSingleViewModel> Singles { get; set; }
    }
}
