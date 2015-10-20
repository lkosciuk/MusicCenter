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
            this.HasKey(a => a.Id);

            this.Property(a => a.name).HasMaxLength(20).IsRequired();
            this.Property(a => a.description).HasMaxLength(1000).IsOptional();

            //relationships
            this.HasMany(a => a.images).WithOptional(a => a.tour);
            this.HasMany(a => a.concerts).WithOptional(a => a.tour);
            //this.HasMany(a => a.users).WithMany(a => a.tours);
            this.HasRequired(a => a.band).WithMany(a => a.tours);
            this.HasMany(a => a.favourites).WithMany(a => a.tours);
            //configure table map
            this.ToTable("Tour");
        }
    }
}
