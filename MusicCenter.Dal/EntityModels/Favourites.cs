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
        //public int UsersID { get; set; }
        public virtual Users user { get; set; }
    }
}
