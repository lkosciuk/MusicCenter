using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class AlbumConfiguration : BaseEntityMap<Album>
    {
        public AlbumConfiguration()
            : base()
        {
            this.ToTable("Album");

            this.Property(a => a.name).HasMaxLength(100).IsRequired();
            this.Property(a => a.releaseDate).IsRequired();
            this.Property(a => a.duration).HasMaxLength(50);
            this.Property(a => a.label).HasMaxLength(50);
            this.Property(a => a.producer).HasMaxLength(50);
            
            //relationships
            this.HasRequired(t => t.band)
                 .WithMany(t => t.albums)
                 .HasForeignKey(d => d.BandID);
        }
    }
}
