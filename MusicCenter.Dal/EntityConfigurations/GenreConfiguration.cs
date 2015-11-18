using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
            : base()
        {
            this.ToTable("Genre");

            this.Property(a => a.name).HasMaxLength(20).IsRequired();

            //relationships
            this.HasMany(t => t.tracks)
                .WithMany(t => t.genres)
                .Map(m =>
                {
                    m.ToTable("TrackGenre");
                    m.MapLeftKey("TrackID");
                    m.MapRightKey("GenreID");
                });

            this.HasMany(t => t.bands)
                .WithMany(t => t.genres)
                .Map(m =>
                {
                    m.ToTable("BandGenre");
                    m.MapLeftKey("BandID");
                    m.MapRightKey("GenreID");
                });

            this.HasMany(t => t.albums)
                .WithMany(t => t.genres)
                .Map(m =>
                {
                    m.ToTable("AlbumGenre");
                    m.MapLeftKey("AlbumID");
                    m.MapRightKey("GenreID");
                });
        }
    }
}
