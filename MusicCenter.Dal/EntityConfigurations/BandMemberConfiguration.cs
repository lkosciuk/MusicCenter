using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class BandMemberConfiguration : EntityTypeConfiguration<BandMember>
    {
        public BandMemberConfiguration()
        {
            this.HasKey(a => a.Id);
            this.Property(a => a.fullName).HasMaxLength(50).IsRequired();

            //relationships
            this.HasMany(a => a.bands).WithMany(a => a.members);
            this.HasOptional(a => a.user).WithOptionalPrincipal(a => a.bandMember);

            //configure table map
            this.ToTable("BandMember");
        }
    }
}
