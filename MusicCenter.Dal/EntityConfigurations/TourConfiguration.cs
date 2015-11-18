using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class TourConfiguration : EntityTypeConfiguration<Tour>
    {
        public TourConfiguration()
        {
            this.ToTable("Tour");

            this.Property(a => a.name).HasMaxLength(20).IsRequired();
            this.Property(a => a.description).HasMaxLength(1000).IsOptional();

            //relationships
            this.HasRequired(t => t.band)
                 .WithMany(t => t.tours)
                 .HasForeignKey(d => d.BandID);
        }
    }
}
