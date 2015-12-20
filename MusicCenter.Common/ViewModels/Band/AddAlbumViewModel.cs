using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MusicCenter.Common.ViewModels.Band
{
    public class AddAlbumViewModel
    {
        public string BandName { get; set; }

        public string Name { get; set; }

        public string ReleaseDate { get; set; }

        public string Duration { get; set; }

        public string Label { get; set; }

        public string Producer { get; set; }

        public string Genres { get; set; }

        public FileViewModel Cover { get; set; }

        public List<AddSingleViewModel> Songs { get; set; }

        

        
    }
}
