using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public abstract class BaseEntity
        : IObjectState
    {
        public int Id { get; set; }

        public ObjectState ObjectState { get; set; }
    }
}
