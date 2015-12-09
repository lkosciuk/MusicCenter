using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class BandMember
        : BaseEntity
    {
        public string fullName { get; set; }
        public virtual ICollection<Band> bands { get; set; }

        public BandMember()
        {
            bands = new List<Band>();
        }
    }
}
