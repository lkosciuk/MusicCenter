using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Concert
{
    public class ConcertListItem
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public FileViewModel Cover { get; set; }
        public int InterestedCount { get; set; }
    }
}
