using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Common
{
    public class SearchItemDetailsViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Label { get; set; }
        public string SubLabel { get; set; }
        public string Date { get; set; }
    }
}
