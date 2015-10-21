using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }

        public Role()
        {
            Users = new List<Users>();
        }
    }
}
