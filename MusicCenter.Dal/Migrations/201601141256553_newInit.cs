namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        releaseDate = c.DateTime(nullable: false),
                        duration = c.String(),
                        label = c.String(),
                        producer = c.String(),
                        BandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Band", t => t.BandID)
                .Index(t => t.BandID);
            
            CreateTable(
                "dbo.Band",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false),
                        name = c.String(nullable: false),
                        description = c.String(),
                        phoneNumber = c.String(),
                        addDate = c.DateTime(nullable: false),
                        bandCreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        bandResolveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Concert",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        address = c.String(nullable: false),
                        description = c.String(),
                        coordinatesX = c.Single(),
                        coordinatesY = c.Single(),
                        TourID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.TourID)
                .Index(t => t.TourID);
            
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        user_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Users", t => t.user_Id)
                .Index(t => t.Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Tour",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(),
                        BandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Band", t => t.BandID)
                .Index(t => t.BandID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        path = c.String(nullable: false),
                        UserID = c.Int(),
                        BandID = c.Int(),
                        TourID = c.Int(),
                        ConcertID = c.Int(),
                        AlbumID = c.Int(),
                        IsAvatar = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Album", t => t.AlbumID)
                .ForeignKey("dbo.Band", t => t.BandID)
                .ForeignKey("dbo.Concert", t => t.ConcertID)
                .ForeignKey("dbo.Tour", t => t.TourID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.BandID)
                .Index(t => t.TourID)
                .Index(t => t.ConcertID)
                .Index(t => t.AlbumID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        password = c.String(),
                        email = c.String(nullable: false),
                        firstName = c.String(),
                        lastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        content = c.String(nullable: false),
                        sentDate = c.DateTime(),
                        isReaded = c.Boolean(nullable: false),
                        UserID = c.Int(),
                        BandID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Band", t => t.BandID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.BandID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Track",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        duration = c.String(),
                        url = c.String(),
                        releaseDate = c.DateTime(nullable: false),
                        BandID = c.Int(nullable: false),
                        IsSingle = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Band", t => t.BandID)
                .Index(t => t.BandID);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BandMember",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        fullName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BandConcert",
                c => new
                    {
                        BandID = c.Int(nullable: false),
                        ConcertID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BandID, t.ConcertID })
                .ForeignKey("dbo.Concert", t => t.BandID, cascadeDelete: true)
                .ForeignKey("dbo.Band", t => t.ConcertID, cascadeDelete: true)
                .Index(t => t.BandID)
                .Index(t => t.ConcertID);
            
            CreateTable(
                "dbo.AlbumFavourites",
                c => new
                    {
                        AlbumID = c.Int(nullable: false),
                        FavouritesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlbumID, t.FavouritesID })
                .ForeignKey("dbo.Favourites", t => t.AlbumID, cascadeDelete: true)
                .ForeignKey("dbo.Album", t => t.FavouritesID, cascadeDelete: true)
                .Index(t => t.AlbumID)
                .Index(t => t.FavouritesID);
            
            CreateTable(
                "dbo.BandFavourites",
                c => new
                    {
                        BandID = c.Int(nullable: false),
                        FavouritesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BandID, t.FavouritesID })
                .ForeignKey("dbo.Favourites", t => t.BandID, cascadeDelete: true)
                .ForeignKey("dbo.Band", t => t.FavouritesID, cascadeDelete: true)
                .Index(t => t.BandID)
                .Index(t => t.FavouritesID);
            
            CreateTable(
                "dbo.ConcertFavourites",
                c => new
                    {
                        ConcertID = c.Int(nullable: false),
                        FavouritesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConcertID, t.FavouritesID })
                .ForeignKey("dbo.Favourites", t => t.ConcertID, cascadeDelete: true)
                .ForeignKey("dbo.Concert", t => t.FavouritesID, cascadeDelete: true)
                .Index(t => t.ConcertID)
                .Index(t => t.FavouritesID);
            
            CreateTable(
                "dbo.MessageBand",
                c => new
                    {
                        MessageID = c.Int(nullable: false),
                        BandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageID, t.BandID })
                .ForeignKey("dbo.Message", t => t.MessageID, cascadeDelete: true)
                .ForeignKey("dbo.Band", t => t.BandID, cascadeDelete: true)
                .Index(t => t.MessageID)
                .Index(t => t.BandID);
            
            CreateTable(
                "dbo.MessageUser",
                c => new
                    {
                        MessageID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageID, t.UserID })
                .ForeignKey("dbo.Message", t => t.MessageID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.MessageID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.RoleUser",
                c => new
                    {
                        RoleID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleID, t.UserID })
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.TourFavourites",
                c => new
                    {
                        TourID = c.Int(nullable: false),
                        FavouritesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TourID, t.FavouritesID })
                .ForeignKey("dbo.Favourites", t => t.TourID, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.FavouritesID, cascadeDelete: true)
                .Index(t => t.TourID)
                .Index(t => t.FavouritesID);
            
            CreateTable(
                "dbo.AlbumTrack",
                c => new
                    {
                        AlbumID = c.Int(nullable: false),
                        TrackID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlbumID, t.TrackID })
                .ForeignKey("dbo.Track", t => t.AlbumID, cascadeDelete: true)
                .ForeignKey("dbo.Album", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.AlbumID)
                .Index(t => t.TrackID);
            
            CreateTable(
                "dbo.AlbumGenre",
                c => new
                    {
                        AlbumID = c.Int(nullable: false),
                        GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlbumID, t.GenreID })
                .ForeignKey("dbo.Genre", t => t.AlbumID, cascadeDelete: true)
                .ForeignKey("dbo.Album", t => t.GenreID, cascadeDelete: true)
                .Index(t => t.AlbumID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.BandGenre",
                c => new
                    {
                        BandID = c.Int(nullable: false),
                        GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BandID, t.GenreID })
                .ForeignKey("dbo.Genre", t => t.BandID, cascadeDelete: true)
                .ForeignKey("dbo.Band", t => t.GenreID, cascadeDelete: true)
                .Index(t => t.BandID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.TrackGenre",
                c => new
                    {
                        TrackID = c.Int(nullable: false),
                        GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrackID, t.GenreID })
                .ForeignKey("dbo.Genre", t => t.TrackID, cascadeDelete: true)
                .ForeignKey("dbo.Track", t => t.GenreID, cascadeDelete: true)
                .Index(t => t.TrackID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.TrackFavourites",
                c => new
                    {
                        TrackID = c.Int(nullable: false),
                        FavouritesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrackID, t.FavouritesID })
                .ForeignKey("dbo.Favourites", t => t.TrackID, cascadeDelete: true)
                .ForeignKey("dbo.Track", t => t.FavouritesID, cascadeDelete: true)
                .Index(t => t.TrackID)
                .Index(t => t.FavouritesID);
            
            CreateTable(
                "dbo.BandToBandMember",
                c => new
                    {
                        BandID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BandID, t.MemberID })
                .ForeignKey("dbo.Band", t => t.BandID, cascadeDelete: true)
                .ForeignKey("dbo.BandMember", t => t.MemberID, cascadeDelete: true)
                .Index(t => t.BandID)
                .Index(t => t.MemberID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Album", "BandID", "dbo.Band");
            DropForeignKey("dbo.Band", "UserID", "dbo.Users");
            DropForeignKey("dbo.BandToBandMember", "MemberID", "dbo.BandMember");
            DropForeignKey("dbo.BandToBandMember", "BandID", "dbo.Band");
            DropForeignKey("dbo.Concert", "TourID", "dbo.Tour");
            DropForeignKey("dbo.Favourites", "user_Id", "dbo.Users");
            DropForeignKey("dbo.TrackFavourites", "FavouritesID", "dbo.Track");
            DropForeignKey("dbo.TrackFavourites", "TrackID", "dbo.Favourites");
            DropForeignKey("dbo.TrackGenre", "GenreID", "dbo.Track");
            DropForeignKey("dbo.TrackGenre", "TrackID", "dbo.Genre");
            DropForeignKey("dbo.BandGenre", "GenreID", "dbo.Band");
            DropForeignKey("dbo.BandGenre", "BandID", "dbo.Genre");
            DropForeignKey("dbo.AlbumGenre", "GenreID", "dbo.Album");
            DropForeignKey("dbo.AlbumGenre", "AlbumID", "dbo.Genre");
            DropForeignKey("dbo.Track", "BandID", "dbo.Band");
            DropForeignKey("dbo.AlbumTrack", "TrackID", "dbo.Album");
            DropForeignKey("dbo.AlbumTrack", "AlbumID", "dbo.Track");
            DropForeignKey("dbo.TourFavourites", "FavouritesID", "dbo.Tour");
            DropForeignKey("dbo.TourFavourites", "TourID", "dbo.Favourites");
            DropForeignKey("dbo.Files", "UserID", "dbo.Users");
            DropForeignKey("dbo.RoleUser", "UserID", "dbo.Users");
            DropForeignKey("dbo.RoleUser", "RoleID", "dbo.Role");
            DropForeignKey("dbo.MessageUser", "UserID", "dbo.Users");
            DropForeignKey("dbo.MessageUser", "MessageID", "dbo.Message");
            DropForeignKey("dbo.Message", "UserID", "dbo.Users");
            DropForeignKey("dbo.MessageBand", "BandID", "dbo.Band");
            DropForeignKey("dbo.MessageBand", "MessageID", "dbo.Message");
            DropForeignKey("dbo.Message", "BandID", "dbo.Band");
            DropForeignKey("dbo.Favourites", "Id", "dbo.Users");
            DropForeignKey("dbo.Files", "TourID", "dbo.Tour");
            DropForeignKey("dbo.Files", "ConcertID", "dbo.Concert");
            DropForeignKey("dbo.Files", "BandID", "dbo.Band");
            DropForeignKey("dbo.Files", "AlbumID", "dbo.Album");
            DropForeignKey("dbo.Tour", "BandID", "dbo.Band");
            DropForeignKey("dbo.ConcertFavourites", "FavouritesID", "dbo.Concert");
            DropForeignKey("dbo.ConcertFavourites", "ConcertID", "dbo.Favourites");
            DropForeignKey("dbo.BandFavourites", "FavouritesID", "dbo.Band");
            DropForeignKey("dbo.BandFavourites", "BandID", "dbo.Favourites");
            DropForeignKey("dbo.AlbumFavourites", "FavouritesID", "dbo.Album");
            DropForeignKey("dbo.AlbumFavourites", "AlbumID", "dbo.Favourites");
            DropForeignKey("dbo.BandConcert", "ConcertID", "dbo.Band");
            DropForeignKey("dbo.BandConcert", "BandID", "dbo.Concert");
            DropIndex("dbo.BandToBandMember", new[] { "MemberID" });
            DropIndex("dbo.BandToBandMember", new[] { "BandID" });
            DropIndex("dbo.TrackFavourites", new[] { "FavouritesID" });
            DropIndex("dbo.TrackFavourites", new[] { "TrackID" });
            DropIndex("dbo.TrackGenre", new[] { "GenreID" });
            DropIndex("dbo.TrackGenre", new[] { "TrackID" });
            DropIndex("dbo.BandGenre", new[] { "GenreID" });
            DropIndex("dbo.BandGenre", new[] { "BandID" });
            DropIndex("dbo.AlbumGenre", new[] { "GenreID" });
            DropIndex("dbo.AlbumGenre", new[] { "AlbumID" });
            DropIndex("dbo.AlbumTrack", new[] { "TrackID" });
            DropIndex("dbo.AlbumTrack", new[] { "AlbumID" });
            DropIndex("dbo.TourFavourites", new[] { "FavouritesID" });
            DropIndex("dbo.TourFavourites", new[] { "TourID" });
            DropIndex("dbo.RoleUser", new[] { "UserID" });
            DropIndex("dbo.RoleUser", new[] { "RoleID" });
            DropIndex("dbo.MessageUser", new[] { "UserID" });
            DropIndex("dbo.MessageUser", new[] { "MessageID" });
            DropIndex("dbo.MessageBand", new[] { "BandID" });
            DropIndex("dbo.MessageBand", new[] { "MessageID" });
            DropIndex("dbo.ConcertFavourites", new[] { "FavouritesID" });
            DropIndex("dbo.ConcertFavourites", new[] { "ConcertID" });
            DropIndex("dbo.BandFavourites", new[] { "FavouritesID" });
            DropIndex("dbo.BandFavourites", new[] { "BandID" });
            DropIndex("dbo.AlbumFavourites", new[] { "FavouritesID" });
            DropIndex("dbo.AlbumFavourites", new[] { "AlbumID" });
            DropIndex("dbo.BandConcert", new[] { "ConcertID" });
            DropIndex("dbo.BandConcert", new[] { "BandID" });
            DropIndex("dbo.Track", new[] { "BandID" });
            DropIndex("dbo.Message", new[] { "BandID" });
            DropIndex("dbo.Message", new[] { "UserID" });
            DropIndex("dbo.Files", new[] { "AlbumID" });
            DropIndex("dbo.Files", new[] { "ConcertID" });
            DropIndex("dbo.Files", new[] { "TourID" });
            DropIndex("dbo.Files", new[] { "BandID" });
            DropIndex("dbo.Files", new[] { "UserID" });
            DropIndex("dbo.Tour", new[] { "BandID" });
            DropIndex("dbo.Favourites", new[] { "user_Id" });
            DropIndex("dbo.Favourites", new[] { "Id" });
            DropIndex("dbo.Concert", new[] { "TourID" });
            DropIndex("dbo.Band", new[] { "UserID" });
            DropIndex("dbo.Album", new[] { "BandID" });
            DropTable("dbo.BandToBandMember");
            DropTable("dbo.TrackFavourites");
            DropTable("dbo.TrackGenre");
            DropTable("dbo.BandGenre");
            DropTable("dbo.AlbumGenre");
            DropTable("dbo.AlbumTrack");
            DropTable("dbo.TourFavourites");
            DropTable("dbo.RoleUser");
            DropTable("dbo.MessageUser");
            DropTable("dbo.MessageBand");
            DropTable("dbo.ConcertFavourites");
            DropTable("dbo.BandFavourites");
            DropTable("dbo.AlbumFavourites");
            DropTable("dbo.BandConcert");
            DropTable("dbo.BandMember");
            DropTable("dbo.Genre");
            DropTable("dbo.Track");
            DropTable("dbo.Role");
            DropTable("dbo.Message");
            DropTable("dbo.Users");
            DropTable("dbo.Files");
            DropTable("dbo.Tour");
            DropTable("dbo.Favourites");
            DropTable("dbo.Concert");
            DropTable("dbo.Band");
            DropTable("dbo.Album");
        }
    }
}
