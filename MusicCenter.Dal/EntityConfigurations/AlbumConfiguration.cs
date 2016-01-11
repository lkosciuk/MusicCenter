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

            this.Property(a => a.name).IsRequired();
            this.Property(a => a.releaseDate).IsRequired();
            this.Property(a => a.duration);
            this.Property(a => a.label);
            this.Property(a => a.producer);
            
            //relationships
            this.HasRequired(t => t.band)
                 .WithMany(t => t.albums)
                 .HasForeignKey(d => d.BandID);
        }
    }
}
