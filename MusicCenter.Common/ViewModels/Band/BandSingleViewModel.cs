using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class BandSingleViewModel
    {
        public int Id { get; set; }

        public string BandName { get; set; }

        public string Name { get; set; }

        public string UrlAddress { get; set; }

        public int Rating { get; set; }
    }
}
