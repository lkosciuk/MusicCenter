using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Users
        : BaseEntity
    {
        public string password { get; set; }
        public string email { get; set; }
        //public int FileID { get; set; }
        public virtual Files profilePhoto { get; set; }
        //public virtual ICollection<Concert> concerts { get; set; }
        //public virtual ICollection<Tour> tours { get; set; }
        //public virtual ICollection<Users> followed { get; set; }
        //public virtual ICollection<Users> spectators { get; set; }
        //public int FavouritesID { get; set; }
        public virtual Favourites favourites { get; set; }
        public virtual ICollection<Message> receivedMessages { get; set; }
        public virtual ICollection<Message> sentMessages { get; set; }
        //public int bandMemberId { get; set; }
        public virtual BandMember bandMember { get; set; }
        public virtual ICollection<Band> bands { get; set; }
        public virtual ICollection<Role> roles { get; set; }
        //TODO: init collectons..
    }
}
