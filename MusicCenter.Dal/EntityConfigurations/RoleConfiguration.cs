using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class RoleConfiguration : BaseEntityMap<Role>
    {
        public RoleConfiguration()
        {
            this.HasKey(a => a.Id);
            this.HasMany(a => a.Users).WithMany(a => a.roles);
        }
    }
}
