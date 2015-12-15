using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Files
        : BaseEntity
    {
        public string name { get; set; }
        public string path { get; set; }
        public virtual Users user { get; set; }
        public int? UserID { get; set; }
        public int? BandID { get; set; }
        public virtual Band band { get; set; }
        public int? TourID { get; set; }
        public virtual Tour tour { get; set; }
        public int? ConcertID { get; set; }
        public virtual Concert concert { get; set; }
        public virtual Album album { get; set; }
        public int? AlbumID { get; set; }

        public bool IsAvatar { get; set; }
    }
}
