namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationshipFIx : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AlbumFavourites", name: "AlbumID", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AlbumFavourites", name: "FavouritesID", newName: "AlbumID");
            RenameColumn(table: "dbo.BandFavourites", name: "BandID", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.BandFavourites", name: "FavouritesID", newName: "BandID");
            RenameColumn(table: "dbo.ConcertFavourites", name: "ConcertID", newName: "__mig_tmp__2");
            RenameColumn(table: "dbo.ConcertFavourites", name: "FavouritesID", newName: "ConcertID");
            RenameColumn(table: "dbo.TourFavourites", name: "TourID", newName: "__mig_tmp__3");
            RenameColumn(table: "dbo.TourFavourites", name: "FavouritesID", newName: "TourID");
            RenameColumn(table: "dbo.TrackFavourites", name: "TrackID", newName: "__mig_tmp__4");
            RenameColumn(table: "dbo.TrackFavourites", name: "FavouritesID", newName: "TrackID");
            RenameColumn(table: "dbo.AlbumFavourites", name: "__mig_tmp__0", newName: "FavouritesID");
            RenameColumn(table: "dbo.BandFavourites", name: "__mig_tmp__1", newName: "FavouritesID");
            RenameColumn(table: "dbo.ConcertFavourites", name: "__mig_tmp__2", newName: "FavouritesID");
            RenameColumn(table: "dbo.TourFavourites", name: "__mig_tmp__3", newName: "FavouritesID");
            RenameColumn(table: "dbo.TrackFavourites", name: "__mig_tmp__4", newName: "FavouritesID");
            RenameIndex(table: "dbo.AlbumFavourites", name: "IX_AlbumID", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.AlbumFavourites", name: "IX_FavouritesID", newName: "IX_AlbumID");
            RenameIndex(table: "dbo.BandFavourites", name: "IX_BandID", newName: "__mig_tmp__1");
            RenameIndex(table: "dbo.BandFavourites", name: "IX_FavouritesID", newName: "IX_BandID");
            RenameIndex(table: "dbo.ConcertFavourites", name: "IX_ConcertID", newName: "__mig_tmp__2");
            RenameIndex(table: "dbo.ConcertFavourites", name: "IX_FavouritesID", newName: "IX_ConcertID");
            RenameIndex(table: "dbo.TourFavourites", name: "IX_TourID", newName: "__mig_tmp__3");
            RenameIndex(table: "dbo.TourFavourites", name: "IX_FavouritesID", newName: "IX_TourID");
            RenameIndex(table: "dbo.TrackFavourites", name: "IX_TrackID", newName: "__mig_tmp__4");
            RenameIndex(table: "dbo.TrackFavourites", name: "IX_FavouritesID", newName: "IX_TrackID");
            RenameIndex(table: "dbo.AlbumFavourites", name: "__mig_tmp__0", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.BandFavourites", name: "__mig_tmp__1", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.ConcertFavourites", name: "__mig_tmp__2", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.TourFavourites", name: "__mig_tmp__3", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.TrackFavourites", name: "__mig_tmp__4", newName: "IX_FavouritesID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TrackFavourites", name: "IX_FavouritesID", newName: "__mig_tmp__4");
            RenameIndex(table: "dbo.TourFavourites", name: "IX_FavouritesID", newName: "__mig_tmp__3");
            RenameIndex(table: "dbo.ConcertFavourites", name: "IX_FavouritesID", newName: "__mig_tmp__2");
            RenameIndex(table: "dbo.BandFavourites", name: "IX_FavouritesID", newName: "__mig_tmp__1");
            RenameIndex(table: "dbo.AlbumFavourites", name: "IX_FavouritesID", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.TrackFavourites", name: "IX_TrackID", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.TrackFavourites", name: "__mig_tmp__4", newName: "IX_TrackID");
            RenameIndex(table: "dbo.TourFavourites", name: "IX_TourID", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.TourFavourites", name: "__mig_tmp__3", newName: "IX_TourID");
            RenameIndex(table: "dbo.ConcertFavourites", name: "IX_ConcertID", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.ConcertFavourites", name: "__mig_tmp__2", newName: "IX_ConcertID");
            RenameIndex(table: "dbo.BandFavourites", name: "IX_BandID", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.BandFavourites", name: "__mig_tmp__1", newName: "IX_BandID");
            RenameIndex(table: "dbo.AlbumFavourites", name: "IX_AlbumID", newName: "IX_FavouritesID");
            RenameIndex(table: "dbo.AlbumFavourites", name: "__mig_tmp__0", newName: "IX_AlbumID");
            RenameColumn(table: "dbo.TrackFavourites", name: "FavouritesID", newName: "__mig_tmp__4");
            RenameColumn(table: "dbo.TourFavourites", name: "FavouritesID", newName: "__mig_tmp__3");
            RenameColumn(table: "dbo.ConcertFavourites", name: "FavouritesID", newName: "__mig_tmp__2");
            RenameColumn(table: "dbo.BandFavourites", name: "FavouritesID", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.AlbumFavourites", name: "FavouritesID", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.TrackFavourites", name: "TrackID", newName: "FavouritesID");
            RenameColumn(table: "dbo.TrackFavourites", name: "__mig_tmp__4", newName: "TrackID");
            RenameColumn(table: "dbo.TourFavourites", name: "TourID", newName: "FavouritesID");
            RenameColumn(table: "dbo.TourFavourites", name: "__mig_tmp__3", newName: "TourID");
            RenameColumn(table: "dbo.ConcertFavourites", name: "ConcertID", newName: "FavouritesID");
            RenameColumn(table: "dbo.ConcertFavourites", name: "__mig_tmp__2", newName: "ConcertID");
            RenameColumn(table: "dbo.BandFavourites", name: "BandID", newName: "FavouritesID");
            RenameColumn(table: "dbo.BandFavourites", name: "__mig_tmp__1", newName: "BandID");
            RenameColumn(table: "dbo.AlbumFavourites", name: "AlbumID", newName: "FavouritesID");
            RenameColumn(table: "dbo.AlbumFavourites", name: "__mig_tmp__0", newName: "AlbumID");
        }
    }
}
