using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class AddSingleViewModel
    {
        public string BandName { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Duration { get; set; }

        public string UrlAddress { get; set; }
    }
}
