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
                    m.MapLeftKey("BandID");
                    m.MapRightKey("FavouritesID");
                });

            this.HasMany(t => t.albums)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("AlbumFavourites");
                    m.MapLeftKey("AlbumID");
                    m.MapRightKey("FavouritesID");
                });

            this.HasMany(t => t.tracks)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("TrackFavourites");
                    m.MapLeftKey("TrackID");
                    m.MapRightKey("FavouritesID");
                });

            this.HasMany(t => t.concerts)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("ConcertFavourites");
                    m.MapLeftKey("ConcertID");
                    m.MapRightKey("FavouritesID");
                });

            this.HasMany(t => t.tours)
                .WithMany(t => t.favourites)
                .Map(m =>
                {
                    m.ToTable("TourFavourites");
                    m.MapLeftKey("TourID");
                    m.MapRightKey("FavouritesID");
                });
        }
    }
}
