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
            : base()
        {
            this.ToTable("Role");

            this.HasMany(t => t.Users)
                .WithMany(t => t.roles)
                .Map(m =>
                {
                    m.ToTable("RoleUser");
                    m.MapLeftKey("RoleID");
                    m.MapRightKey("UserID");
                });
        }
    }
}
