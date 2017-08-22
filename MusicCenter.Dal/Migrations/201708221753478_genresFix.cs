namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genresFix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AlbumGenre", name: "AlbumID", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AlbumGenre", name: "GenreID", newName: "AlbumID");
            RenameColumn(table: "dbo.BandGenre", name: "BandID", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.BandGenre", name: "GenreID", newName: "BandID");
            RenameColumn(table: "dbo.TrackGenre", name: "TrackID", newName: "__mig_tmp__2");
            RenameColumn(table: "dbo.TrackGenre", name: "GenreID", newName: "TrackID");
            RenameColumn(table: "dbo.AlbumGenre", name: "__mig_tmp__0", newName: "GenreID");
            RenameColumn(table: "dbo.BandGenre", name: "__mig_tmp__1", newName: "GenreID");
            RenameColumn(table: "dbo.TrackGenre", name: "__mig_tmp__2", newName: "GenreID");
            RenameIndex(table: "dbo.AlbumGenre", name: "IX_AlbumID", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.AlbumGenre", name: "IX_GenreID", newName: "IX_AlbumID");
            RenameIndex(table: "dbo.BandGenre", name: "IX_BandID", newName: "__mig_tmp__1");
            RenameIndex(table: "dbo.BandGenre", name: "IX_GenreID", newName: "IX_BandID");
            RenameIndex(table: "dbo.TrackGenre", name: "IX_TrackID", newName: "__mig_tmp__2");
            RenameIndex(table: "dbo.TrackGenre", name: "IX_GenreID", newName: "IX_TrackID");
            RenameIndex(table: "dbo.AlbumGenre", name: "__mig_tmp__0", newName: "IX_GenreID");
            RenameIndex(table: "dbo.BandGenre", name: "__mig_tmp__1", newName: "IX_GenreID");
            RenameIndex(table: "dbo.TrackGenre", name: "__mig_tmp__2", newName: "IX_GenreID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TrackGenre", name: "IX_GenreID", newName: "__mig_tmp__2");
            RenameIndex(table: "dbo.BandGenre", name: "IX_GenreID", newName: "__mig_tmp__1");
            RenameIndex(table: "dbo.AlbumGenre", name: "IX_GenreID", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.TrackGenre", name: "IX_TrackID", newName: "IX_GenreID");
            RenameIndex(table: "dbo.TrackGenre", name: "__mig_tmp__2", newName: "IX_TrackID");
            RenameIndex(table: "dbo.BandGenre", name: "IX_BandID", newName: "IX_GenreID");
            RenameIndex(table: "dbo.BandGenre", name: "__mig_tmp__1", newName: "IX_BandID");
            RenameIndex(table: "dbo.AlbumGenre", name: "IX_AlbumID", newName: "IX_GenreID");
            RenameIndex(table: "dbo.AlbumGenre", name: "__mig_tmp__0", newName: "IX_AlbumID");
            RenameColumn(table: "dbo.TrackGenre", name: "GenreID", newName: "__mig_tmp__2");
            RenameColumn(table: "dbo.BandGenre", name: "GenreID", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.AlbumGenre", name: "GenreID", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.TrackGenre", name: "TrackID", newName: "GenreID");
            RenameColumn(table: "dbo.TrackGenre", name: "__mig_tmp__2", newName: "TrackID");
            RenameColumn(table: "dbo.BandGenre", name: "BandID", newName: "GenreID");
            RenameColumn(table: "dbo.BandGenre", name: "__mig_tmp__1", newName: "BandID");
            RenameColumn(table: "dbo.AlbumGenre", name: "AlbumID", newName: "GenreID");
            RenameColumn(table: "dbo.AlbumGenre", name: "__mig_tmp__0", newName: "AlbumID");
        }
    }
}
