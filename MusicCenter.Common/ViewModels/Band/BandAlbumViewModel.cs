﻿using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class BandAlbumViewModel
    {
        public string BandName { get; set; }

        public FileViewModel Cover { get; set; } 
       
        public string Name {get; set;}

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ReleaseDate { get; set; }

        public int Rating { get; set; }

        public string[] Genres { get; set; }

    }
}
