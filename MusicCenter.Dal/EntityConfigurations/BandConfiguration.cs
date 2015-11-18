using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class BandConfiguration : EntityTypeConfiguration<Band>
    {
        public BandConfiguration()
            : base()
        {
            this.ToTable("Band");

            this.Property(a => a.email).HasMaxLength(15).IsRequired();
            this.Property(a => a.name).HasMaxLength(20).IsRequired();
            this.Property(a => a.description).HasMaxLength(1000);
            this.Property(a => a.phoneNumber).HasMaxLength(15);

            //relationships
            this.HasRequired(t => t.user)
                 .WithMany(t => t.bands)
                 .HasForeignKey(d => d.UserID);

            this.HasMany(t => t.members)
                .WithMany(t => t.bands)
                .Map(m =>
                {
                    m.ToTable("BandToBandMember");
                    m.MapLeftKey("BandID");
                    m.MapRightKey("MemberID");
                });
        }
    }
}
