using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class AlbumConfiguration : EntityTypeConfiguration<Album>
    {
        public AlbumConfiguration()
        {
            //this.HasKey(a => a.Id);
            this.Property(a => a.name).HasMaxLength(100).IsRequired();
            this.Property(a => a.releaseDate).IsRequired();
            this.Property(a => a.duration).HasMaxLength(50).IsRequired();
            this.Property(a => a.label).HasMaxLength(50).IsRequired();
            this.Property(a => a.producer).HasMaxLength(50).IsRequired();
            
            //relationships
            this.HasRequired(a => a.band).WithMany(a => a.albums);
            this.HasMany(a => a.favourites).WithMany(a => a.albums);
            this.HasMany(a => a.genres).WithMany(a => a.albums);
            this.HasMany(a => a.trackList).WithMany(a => a.albums);
            

            //configure table map
            this.ToTable("Album");
        }
    }
}
