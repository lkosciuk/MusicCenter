using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Album
        : BaseEntity
    {
        public string name { get; set; }
        public virtual ICollection<Files> images {get; set;}
        public DateTime releaseDate { get; set; }
        public string duration { get; set; }
        public virtual ICollection<Genre> genres { get; set; }
        public virtual ICollection<Track> trackList { get; set; }
        public string label { get; set; } //wytwornia
        public string producer { get; set; }
        public virtual ICollection<Favourites> favourites { get; set; }
        public int BandID { get; set; }
        public virtual Band band { get; set; }
        public int rating { get; set; }

        public Album()
        {
            images = new List<Files>();
            genres = new List<Genre>();
            trackList = new List<Track>();
            favourites = new List<Favourites>();
        }
        
    }
}
