using MusicCenter.Common.ViewModels.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Band
{
    public class BandListItemViewModel
    {
        public FileViewModel Avatar { get; set; }

        public string Name { get; set; }

        public List<string> Genres { get; set; }

        public string Description { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ResolveDate { get; set; }
    }
}
