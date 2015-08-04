using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class BaseEntityMap<Entity>
        : EntityTypeConfiguration<Entity> where Entity : BaseEntity
    {
        public BaseEntityMap()
        {
            this.HasKey(a => a.Id);
        }
    }
}
