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
        public string ticketPrice { get; set; }
        public string ticketUrl { get; set; }
        public string address { get; set; }
        public float coordinatesX { get; set; }
        public float coordinatesY { get; set; }
        public virtual ICollection<Files> images { get; set; }
        public virtual ICollection<Favourites> favouritess { get; set; }
        //public int TourID { get; set; }
        public virtual Tour tour { get; set; }
        //public int BandID { get; set; }
        public virtual Band band { get; set; }


    }
}
