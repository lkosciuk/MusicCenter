using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCenter.Common.ViewModels.Band
{
    public class AlbumListItemViewModel
    {
        public FileViewModel Avatar { get; set; }

        public string Name { get; set; }

        public List<string> Genres { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreationDate { get; set; }
    }
}
