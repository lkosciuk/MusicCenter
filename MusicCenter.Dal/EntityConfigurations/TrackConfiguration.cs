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
            this.ToTable("Track");

            this.Property(a => a.name).HasMaxLength(20).IsRequired();
            this.Property(a => a.duration).HasMaxLength(10).IsOptional();
            this.Property(a => a.url).HasMaxLength(200).IsOptional();


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
