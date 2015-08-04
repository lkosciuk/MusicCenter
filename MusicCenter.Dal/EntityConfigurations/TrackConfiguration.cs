using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class TrackConfiguration : EntityTypeConfiguration<Track>
    {
        public TrackConfiguration()
        {
            this.HasKey(a => a.Id);

            this.Property(a => a.name).HasMaxLength(20).IsRequired();
            this.Property(a => a.duration).HasMaxLength(10).IsOptional();
            this.Property(a => a.url).HasMaxLength(200).IsOptional();


            //relationships
            this.HasMany(a => a.favourites).WithMany(a => a.tracks);
            this.HasMany(a => a.albums).WithMany(a => a.trackList);
            this.HasOptional(a => a.band).WithMany(a => a.singles);

            //configure table map
            this.ToTable("Track");
        }
    }
}
