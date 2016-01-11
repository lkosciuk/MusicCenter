using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class TrackConfiguration : BaseEntityMap<Track>
    {
        public TrackConfiguration()
            : base()
        {
            this.ToTable("Track");

            this.Property(a => a.name).IsRequired();
            this.Property(a => a.duration).IsOptional();
            this.Property(a => a.url).IsOptional();


            //relationships
            this.HasRequired(t => t.band)
                 .WithMany(t => t.singles)
                 .HasForeignKey(d => d.BandID);

            this.HasMany(t => t.albums)
                .WithMany(t => t.trackList)
                .Map(m =>
                {
                    m.ToTable("AlbumTrack");
                    m.MapLeftKey("AlbumID");
                    m.MapRightKey("TrackID");
                });
        }
    }
}
