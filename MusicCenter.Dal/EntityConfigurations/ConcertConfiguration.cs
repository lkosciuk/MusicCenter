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
            this.Property(a => a.ticketPrice).HasMaxLength(15).IsOptional();
            this.Property(a => a.ticketUrl).HasMaxLength(200).IsOptional();
            this.Property(a => a.address).HasMaxLength(30).IsRequired();
            this.Property(a => a.coordinatesX).IsOptional();
            this.Property(a => a.coordinatesY).IsOptional();

            //relationships
            this.HasRequired(t => t.band)
                 .WithMany(t => t.concerts)
                 .HasForeignKey(d => d.BandID);

            this.HasOptional(t => t.tour)
                 .WithMany(t => t.concerts)
                 .HasForeignKey(d => d.TourID);
        }
    }
}
