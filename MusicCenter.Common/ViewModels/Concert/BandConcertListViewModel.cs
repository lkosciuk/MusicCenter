using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Concert
{
    public class BandConcertListViewModel
    {
        public string BandName { get; set; }
        public List<ConcertListItem> Concerts { get; set; }
    }
}
