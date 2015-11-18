using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Message
        : BaseEntity
    {
        public string title { get; set; }
        public string content { get; set; }
        public DateTime sentDate { get; set; }
        public bool isReaded { get; set; }
        public int? UserID { get; set; }
        public virtual Users UserAuthor { get; set; }
        public virtual ICollection<Users> UserReceivers { get; set; }
        public int? BandID { get; set; }
        public virtual Band BandAuthor { get; set; }
        public virtual ICollection<Band> BandReceivers { get; set; }

    }
}
