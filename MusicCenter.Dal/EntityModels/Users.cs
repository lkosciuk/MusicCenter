using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Users : BaseEntity
    {
        public string password { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public virtual ICollection<Files> images { get; set; }
        public virtual Favourites favourites { get; set; }
        public virtual ICollection<Message> receivedMessages { get; set; }
        public virtual ICollection<Message> sentMessages { get; set; }
        public virtual ICollection<Band> bands { get; set; }
        public virtual ICollection<Role> roles { get; set; }

        public Users()
        {
            receivedMessages = new List<Message>();
            sentMessages = new List<Message>();
            bands = new List<Band>();
            roles = new List<Role>();
            images = new List<Files>();
        }
    }
}
