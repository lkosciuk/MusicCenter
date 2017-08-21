using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class FavouritesConfiguration : BaseEntityMap<Favourites>
    {
        public FavouritesConfiguration()
            : base()
        {
            this.ToTable("Favourites");
            this.Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            //relationships
            this.HasMany(t => t.bands)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("BandFavourites");
                    m.MapRightKey("BandID");
                    m.MapLeftKey("FavouritesID");
                });

            this.HasMany(t => t.albums)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("AlbumFavourites");
                    m.MapRightKey("AlbumID");
                    m.MapLeftKey("FavouritesID");
                });

            this.HasMany(t => t.tracks)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("TrackFavourites");
                    m.MapRightKey("TrackID");
                    m.MapLeftKey("FavouritesID");
                });

            this.HasMany(t => t.concerts)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("ConcertFavourites");
                    m.MapRightKey("ConcertID");
                    m.MapLeftKey("FavouritesID");
                });

            this.HasMany(t => t.tours)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("TourFavourites");
                    m.MapRightKey("TourID");
                    m.MapLeftKey("FavouritesID");
                });
        }
    }
}
