using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class AlbumsPanelViewModel
    {
        public string CoverPath { get; set; }
        public string AlbumName { get; set; }
        public string Genres { get; set; }
        public string ReleaseDate { get; set; }
    }
}
