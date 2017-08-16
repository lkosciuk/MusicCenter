using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCenter.Common.ViewModels.Concert
{
    public class ConcertListItemViewModel
    {
        public int Id { get; set; }

        public string ConcertOwner { get; set; }

        public FileViewModel Image { get; set; }

        public List<string> BandNames { get; set; }

        public List<string> Genres { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public string Address { get; set; }
    }
}
