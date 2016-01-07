using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Track
        : BaseEntity
    {
        public string name { get; set; }
        public string duration { get; set; }
        public string url { get; set; }
        public DateTime releaseDate { get; set; }
        public virtual ICollection<Genre> genres { get; set; }
        public virtual ICollection<Favourites> favourites { get; set; }
        public int BandID { get; set; }
        public virtual Band band { get; set; }
        public virtual ICollection<Album> albums { get; set; }
        public bool IsSingle { get; set; }

        public Track()
        {
            genres = new List<Genre>();
            favourites = new List<Favourites>();
            albums = new List<Album>();
        }
    }
}
