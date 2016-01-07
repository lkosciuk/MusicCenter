using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Concert
        : BaseEntity
    {
        public DateTime date { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public float coordinatesX { get; set; }
        public float coordinatesY { get; set; }
        public virtual ICollection<Files> images { get; set; }
        public virtual ICollection<Favourites> favourites { get; set; }
        public int? TourID { get; set; }
        public virtual Tour tour { get; set; }
        public virtual ICollection<Band> bands { get; set; }


    }
}
