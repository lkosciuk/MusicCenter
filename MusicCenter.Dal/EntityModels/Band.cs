using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Band
        : BaseEntity
    {
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string phoneNumber { get; set; }
        public virtual ICollection<BandMember> members { get; set; }
        public virtual ICollection<Files> images { get; set; }
        public virtual ICollection<Genre> genres { get; set; }
        public virtual ICollection<Album> albums { get; set; }
        public virtual ICollection<Track> singles { get; set; }
        public virtual ICollection<Concert> concerts { get; set; }
        public virtual ICollection<Tour> tours { get; set; }
        public virtual ICollection<Message> receivedMessages { get; set; }
        public virtual ICollection<Message> sentMessages { get; set; }
        public virtual ICollection<Favourites> favourites { get; set; }
    }
}
