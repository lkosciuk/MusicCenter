using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Favourites
        : BaseEntity
    {
        public virtual ICollection<Band> bands { get; set; }
        public virtual ICollection<Album> albums { get; set; }
        public virtual ICollection<Track> tracks { get; set; }
        public virtual ICollection<Concert> concerts { get; set; }
        public virtual ICollection<Tour> tours { get; set; }
        public virtual Users user { get; set; }

        public Favourites()
        {
            bands = new List<Band>();
            albums = new List<Album>();
            tracks = new List<Track>();
            concerts = new List<Concert>();
            tours = new List<Tour>();
        }
    }
}
