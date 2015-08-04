namespace MusicCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        releaseDate = c.DateTime(nullable: false),
                        duration = c.String(nullable: false, maxLength: 50),
                        label = c.String(nullable: false, maxLength: 50),
                        producer = c.String(nullable: false, maxLength: 50),
                        band_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Band", t => t.band_Id)
                .Index(t => t.band_Id);
            
            CreateTable(
                "dbo.Band",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        login = c.String(nullable: false, maxLength: 15),
                        password = c.String(nullable: false, maxLength: 15),
                        email = c.String(nullable: false, maxLength: 15),
                        name = c.String(nullable: false, maxLength: 20),
                        description = c.String(maxLength: 1000),
                        phoneNumber = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Concert",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        ticketPrice = c.String(maxLength: 15),
                        ticketUrl = c.String(maxLength: 200),
                        address = c.String(nullable: false, maxLength: 30),
                        coordinatesX = c.Single(),
                        coordinatesY = c.Single(),
                        tour_Id = c.Int(),
                        band_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.tour_Id)
                .ForeignKey("dbo.Band", t => t.band_Id)
                .Index(t => t.tour_Id)
                .Index(t => t.band_Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        path = c.String(nullable: false, maxLength: 100),
                        tour_Id = c.Int(),
                        user_Id = c.Int(),
                        concert_Id = c.Int(),
                        band_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tour", t => t.tour_Id)
                .ForeignKey("dbo.Users", t => t.user_Id)
                .ForeignKey("dbo.Concert", t => t.concert_Id)
                .ForeignKey("dbo.Band", t => t.band_Id)
                .Index(t => t.tour_Id)
                .Index(t => t.user_Id)
                .Index(t => t.concert_Id)
                .Index(t => t.band_Id);
            
            CreateTable(
                "dbo.Tour",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20),
                        description = c.String(maxLength: 1000),
                        band_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Band", t => t.band_Id)
                .Index(t => t.band_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        login = c.String(nullable: false, maxLength: 20),
                        password = c.String(nullable: false, maxLength: 10),
                        email = c.String(nullable: false, maxLength: 20),
                        bandMember_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BandMember", t => t.bandMember_Id)
                .Index(t => t.bandMember_Id);
            
            CreateTable(
                "dbo.BandMember",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        fullName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Track",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20),
                        duration = c.String(maxLength: 10),
                        url = c.String(maxLength: 200),
                        band_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Band", t => t.band_Id)
                .Index(t => t.band_Id);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 50),
                        content = c.String(nullable: false, maxLength: 1000),
                        sentDate = c.DateTime(),
                        isReaded = c.Boolean(nullable: false),
                        UserAuthor_Id = c.Int(),
                        BandAuthor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserAuthor_Id)
                .ForeignKey("dbo.Band", t => t.BandAuthor_Id)
                .Index(t => t.UserAuthor_Id)
                .Index(t => t.BandAuthor_Id);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FavouritesTracks",
                c => new
                    {
                        Favourites_Id = c.Int(nullable: false),
                        Track_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Favourites_Id, t.Track_Id })
                .ForeignKey("dbo.Favourites", t => t.Favourites_Id, cascadeDelete: true)
                .ForeignKey("dbo.Track", t => t.Track_Id, cascadeDelete: true)
                .Index(t => t.Favourites_Id)
                .Index(t => t.Track_Id);
            
            CreateTable(
                "dbo.UsersUsers",
                c => new
                    {
                        Users_Id = c.Int(nullable: false),
                        Users_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Users_Id, t.Users_Id1 })
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .ForeignKey("dbo.Users", t => t.Users_Id1)
                .Index(t => t.Users_Id)
                .Index(t => t.Users_Id1);
            
            CreateTable(
                "dbo.MessageUsers",
                c => new
                    {
                        Message_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Message_Id, t.Users_Id })
                .ForeignKey("dbo.Message", t => t.Message_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Message_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.TourUsers",
                c => new
                    {
                        Tour_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tour_Id, t.Users_Id })
                .ForeignKey("dbo.Tour", t => t.Tour_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Tour_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.ConcertUsers",
                c => new
                    {
                        Concert_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Concert_Id, t.Users_Id })
                .ForeignKey("dbo.Concert", t => t.Concert_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Concert_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.BandFavourites",
                c => new
                    {
                        Band_Id = c.Int(nullable: false),
                        Favourites_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Band_Id, t.Favourites_Id })
                .ForeignKey("dbo.Band", t => t.Band_Id, cascadeDelete: true)
                .ForeignKey("dbo.Favourites", t => t.Favourites_Id, cascadeDelete: true)
                .Index(t => t.Band_Id)
                .Index(t => t.Favourites_Id);
            
            CreateTable(
                "dbo.BandGenres",
                c => new
                    {
                        Band_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Band_Id, t.Genre_Id })
                .ForeignKey("dbo.Band", t => t.Band_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Band_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.BandBandMembers",
                c => new
                    {
                        Band_Id = c.Int(nullable: false),
                        BandMember_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Band_Id, t.BandMember_Id })
                .ForeignKey("dbo.Band", t => t.Band_Id, cascadeDelete: true)
                .ForeignKey("dbo.BandMember", t => t.BandMember_Id, cascadeDelete: true)
                .Index(t => t.Band_Id)
                .Index(t => t.BandMember_Id);
            
            CreateTable(
                "dbo.BandMessages",
                c => new
                    {
                        Band_Id = c.Int(nullable: false),
                        Message_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Band_Id, t.Message_Id })
                .ForeignKey("dbo.Band", t => t.Band_Id, cascadeDelete: true)
                .ForeignKey("dbo.Message", t => t.Message_Id, cascadeDelete: true)
                .Index(t => t.Band_Id)
                .Index(t => t.Message_Id);
            
            CreateTable(
                "dbo.AlbumFavourites",
                c => new
                    {
                        Album_Id = c.Int(nullable: false),
                        Favourites_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Album_Id, t.Favourites_Id })
                .ForeignKey("dbo.Album", t => t.Album_Id, cascadeDelete: true)
                .ForeignKey("dbo.Favourites", t => t.Favourites_Id, cascadeDelete: true)
                .Index(t => t.Album_Id)
                .Index(t => t.Favourites_Id);
            
            CreateTable(
                "dbo.AlbumGenres",
                c => new
                    {
                        Album_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Album_Id, t.Genre_Id })
                .ForeignKey("dbo.Album", t => t.Album_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Album_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.AlbumTracks",
                c => new
                    {
                        Album_Id = c.Int(nullable: false),
                        Track_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Album_Id, t.Track_Id })
                .ForeignKey("dbo.Album", t => t.Album_Id, cascadeDelete: true)
                .ForeignKey("dbo.Track", t => t.Track_Id, cascadeDelete: true)
                .Index(t => t.Album_Id)
                .Index(t => t.Track_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlbumTracks", "Track_Id", "dbo.Track");
            DropForeignKey("dbo.AlbumTracks", "Album_Id", "dbo.Album");
            DropForeignKey("dbo.AlbumGenres", "Genre_Id", "dbo.Genre");
            DropForeignKey("dbo.AlbumGenres", "Album_Id", "dbo.Album");
            DropForeignKey("dbo.AlbumFavourites", "Favourites_Id", "dbo.Favourites");
            DropForeignKey("dbo.AlbumFavourites", "Album_Id", "dbo.Album");
            DropForeignKey("dbo.Album", "band_Id", "dbo.Band");
            DropForeignKey("dbo.Tour", "band_Id", "dbo.Band");
            DropForeignKey("dbo.Track", "band_Id", "dbo.Band");
            DropForeignKey("dbo.Message", "BandAuthor_Id", "dbo.Band");
            DropForeignKey("dbo.BandMessages", "Message_Id", "dbo.Message");
            DropForeignKey("dbo.BandMessages", "Band_Id", "dbo.Band");
            DropForeignKey("dbo.BandBandMembers", "BandMember_Id", "dbo.BandMember");
            DropForeignKey("dbo.BandBandMembers", "Band_Id", "dbo.Band");
            DropForeignKey("dbo.Files", "band_Id", "dbo.Band");
            DropForeignKey("dbo.BandGenres", "Genre_Id", "dbo.Genre");
            DropForeignKey("dbo.BandGenres", "Band_Id", "dbo.Band");
            DropForeignKey("dbo.BandFavourites", "Favourites_Id", "dbo.Favourites");
            DropForeignKey("dbo.BandFavourites", "Band_Id", "dbo.Band");
            DropForeignKey("dbo.Concert", "band_Id", "dbo.Band");
            DropForeignKey("dbo.ConcertUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.ConcertUsers", "Concert_Id", "dbo.Concert");
            DropForeignKey("dbo.Concert", "tour_Id", "dbo.Tour");
            DropForeignKey("dbo.Files", "concert_Id", "dbo.Concert");
            DropForeignKey("dbo.Files", "user_Id", "dbo.Users");
            DropForeignKey("dbo.Files", "tour_Id", "dbo.Tour");
            DropForeignKey("dbo.TourUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.TourUsers", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.MessageUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.MessageUsers", "Message_Id", "dbo.Message");
            DropForeignKey("dbo.Message", "UserAuthor_Id", "dbo.Users");
            DropForeignKey("dbo.UsersUsers", "Users_Id1", "dbo.Users");
            DropForeignKey("dbo.UsersUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.Favourites", "Id", "dbo.Users");
            DropForeignKey("dbo.FavouritesTracks", "Track_Id", "dbo.Track");
            DropForeignKey("dbo.FavouritesTracks", "Favourites_Id", "dbo.Favourites");
            DropForeignKey("dbo.Users", "bandMember_Id", "dbo.BandMember");
            DropIndex("dbo.AlbumTracks", new[] { "Track_Id" });
            DropIndex("dbo.AlbumTracks", new[] { "Album_Id" });
            DropIndex("dbo.AlbumGenres", new[] { "Genre_Id" });
            DropIndex("dbo.AlbumGenres", new[] { "Album_Id" });
            DropIndex("dbo.AlbumFavourites", new[] { "Favourites_Id" });
            DropIndex("dbo.AlbumFavourites", new[] { "Album_Id" });
            DropIndex("dbo.BandMessages", new[] { "Message_Id" });
            DropIndex("dbo.BandMessages", new[] { "Band_Id" });
            DropIndex("dbo.BandBandMembers", new[] { "BandMember_Id" });
            DropIndex("dbo.BandBandMembers", new[] { "Band_Id" });
            DropIndex("dbo.BandGenres", new[] { "Genre_Id" });
            DropIndex("dbo.BandGenres", new[] { "Band_Id" });
            DropIndex("dbo.BandFavourites", new[] { "Favourites_Id" });
            DropIndex("dbo.BandFavourites", new[] { "Band_Id" });
            DropIndex("dbo.ConcertUsers", new[] { "Users_Id" });
            DropIndex("dbo.ConcertUsers", new[] { "Concert_Id" });
            DropIndex("dbo.TourUsers", new[] { "Users_Id" });
            DropIndex("dbo.TourUsers", new[] { "Tour_Id" });
            DropIndex("dbo.MessageUsers", new[] { "Users_Id" });
            DropIndex("dbo.MessageUsers", new[] { "Message_Id" });
            DropIndex("dbo.UsersUsers", new[] { "Users_Id1" });
            DropIndex("dbo.UsersUsers", new[] { "Users_Id" });
            DropIndex("dbo.FavouritesTracks", new[] { "Track_Id" });
            DropIndex("dbo.FavouritesTracks", new[] { "Favourites_Id" });
            DropIndex("dbo.Message", new[] { "BandAuthor_Id" });
            DropIndex("dbo.Message", new[] { "UserAuthor_Id" });
            DropIndex("dbo.Track", new[] { "band_Id" });
            DropIndex("dbo.Favourites", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "bandMember_Id" });
            DropIndex("dbo.Tour", new[] { "band_Id" });
            DropIndex("dbo.Files", new[] { "band_Id" });
            DropIndex("dbo.Files", new[] { "concert_Id" });
            DropIndex("dbo.Files", new[] { "user_Id" });
            DropIndex("dbo.Files", new[] { "tour_Id" });
            DropIndex("dbo.Concert", new[] { "band_Id" });
            DropIndex("dbo.Concert", new[] { "tour_Id" });
            DropIndex("dbo.Album", new[] { "band_Id" });
            DropTable("dbo.AlbumTracks");
            DropTable("dbo.AlbumGenres");
            DropTable("dbo.AlbumFavourites");
            DropTable("dbo.BandMessages");
            DropTable("dbo.BandBandMembers");
            DropTable("dbo.BandGenres");
            DropTable("dbo.BandFavourites");
            DropTable("dbo.ConcertUsers");
            DropTable("dbo.TourUsers");
            DropTable("dbo.MessageUsers");
            DropTable("dbo.UsersUsers");
            DropTable("dbo.FavouritesTracks");
            DropTable("dbo.Genre");
            DropTable("dbo.Message");
            DropTable("dbo.Track");
            DropTable("dbo.Favourites");
            DropTable("dbo.BandMember");
            DropTable("dbo.Users");
            DropTable("dbo.Tour");
            DropTable("dbo.Files");
            DropTable("dbo.Concert");
            DropTable("dbo.Band");
            DropTable("dbo.Album");
        }
    }
}
