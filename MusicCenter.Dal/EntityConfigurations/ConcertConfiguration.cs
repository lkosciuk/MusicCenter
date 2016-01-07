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
            this.Property(a => a.address).HasMaxLength(30).IsRequired();
            this.Property(a => a.coordinatesX).IsOptional();
            this.Property(a => a.coordinatesY).IsOptional();

            //relationships
            this.HasMany(t => t.bands)
                 .WithMany(t => t.concerts).Map(m =>
                 {
                     m.ToTable("BandConcert");
                     m.MapLeftKey("BandID");
                     m.MapRightKey("ConcertID");
                 });

            this.HasOptional(t => t.tour)
                 .WithMany(t => t.concerts)
                 .HasForeignKey(d => d.TourID);
        }
    }
}
