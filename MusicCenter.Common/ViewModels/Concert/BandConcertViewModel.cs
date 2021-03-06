﻿using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Concert
{
    public class BandConcertViewModel
    {
        public string BandName { get; set; }

        public FileViewModel Avatar { get; set; }

        public string Genres { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime CreationDate { get; set; }

        public string Description { get; set; }
    }
}
