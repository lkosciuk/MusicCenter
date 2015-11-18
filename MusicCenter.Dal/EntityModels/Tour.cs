using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Tour
        : BaseEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public virtual ICollection<Files> images { get; set; }
        public virtual ICollection<Concert> concerts {get;set;}
        public virtual ICollection<Favourites> favourites { get; set; }
        public int BandID { get; set; }
        public virtual Band band { get; set; }
    }
}
