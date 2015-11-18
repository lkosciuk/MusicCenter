using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public abstract class BaseEntity
        : Entity
    {
        public int Id { get; set; }
    }
}
