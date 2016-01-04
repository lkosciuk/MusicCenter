using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class AlbumViewModel
    {
        public string BandName { get; set; }

        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ReleaseDate { get; set; }

        public int Rating { get; set; }

        public string Label { get; set; }

        public string Producer { get; set; }

        public string Genres { get; set; }

        public string CoverPath { get; set; }

        public List<BandSingleViewModel> Songs { get; set; }
    }
}
