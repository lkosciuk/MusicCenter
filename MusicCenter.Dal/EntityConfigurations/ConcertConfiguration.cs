using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class ConcertConfiguration : BaseEntityMap<Concert>
    {
        public ConcertConfiguration()
            : base()
        {
            this.ToTable("Concert");

            this.Property(a => a.date).IsRequired();
            this.Property(a => a.address).IsRequired();

            //relationships
            this.HasMany(t => t.bands)
                 .WithMany(t => t.MemberConcerts).Map(m =>
                 {
                     m.ToTable("BandMemberConcert");
                     m.MapLeftKey("BandID");
                     m.MapRightKey("ConcertID");
                 });

            this.HasRequired(t => t.ConcertOwner)
                .WithMany(t => t.OwnedConcerts)
                .HasForeignKey(t => t.BandID);

            this.HasOptional(t => t.tour)
                 .WithMany(t => t.concerts)
                 .HasForeignKey(d => d.TourID);
        }
    }
}
