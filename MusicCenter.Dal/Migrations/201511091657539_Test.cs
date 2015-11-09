namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.UsersUsers", "Users_Id1", "dbo.Users");
            DropForeignKey("dbo.TourUsers", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.TourUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.ConcertUsers", "Concert_Id", "dbo.Concert");
            DropForeignKey("dbo.ConcertUsers", "Users_Id", "dbo.Users");
            DropIndex("dbo.UsersUsers", new[] { "Users_Id" });
            DropIndex("dbo.UsersUsers", new[] { "Users_Id1" });
            DropIndex("dbo.TourUsers", new[] { "Tour_Id" });
            DropIndex("dbo.TourUsers", new[] { "Users_Id" });
            DropIndex("dbo.ConcertUsers", new[] { "Concert_Id" });
            DropIndex("dbo.ConcertUsers", new[] { "Users_Id" });
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ObjectState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Users_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.FavouritesTours",
                c => new
                    {
                        Favourites_Id = c.Int(nullable: false),
                        Tour_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Favourites_Id, t.Tour_Id })
                .ForeignKey("dbo.Favourites", t => t.Favourites_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tour", t => t.Tour_Id, cascadeDelete: true)
                .Index(t => t.Favourites_Id)
                .Index(t => t.Tour_Id);
            
            CreateTable(
                "dbo.TrackGenres",
                c => new
                    {
                        Track_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Track_Id, t.Genre_Id })
                .ForeignKey("dbo.Track", t => t.Track_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Track_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.ConcertFavourites",
                c => new
                    {
                        Concert_Id = c.Int(nullable: false),
                        Favourites_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Concert_Id, t.Favourites_Id })
                .ForeignKey("dbo.Concert", t => t.Concert_Id, cascadeDelete: true)
                .ForeignKey("dbo.Favourites", t => t.Favourites_Id, cascadeDelete: true)
                .Index(t => t.Concert_Id)
                .Index(t => t.Favourites_Id);
            
            AddColumn("dbo.Album", "rating", c => c.Int(nullable: false));
            AddColumn("dbo.Band", "addDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Band", "bandCreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Band", "bandResolveDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Band", "user_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "firstName", c => c.String());
            AddColumn("dbo.Users", "lastName", c => c.String());
            AddColumn("dbo.Track", "releaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Track", "rating", c => c.Int(nullable: false));
            CreateIndex("dbo.Band", "user_Id");
            AddForeignKey("dbo.Band", "user_Id", "dbo.Users", "Id");
            DropColumn("dbo.Band", "login");
            DropColumn("dbo.Band", "password");
            DropColumn("dbo.Users", "login");
            DropTable("dbo.UsersUsers");
            DropTable("dbo.TourUsers");
            DropTable("dbo.ConcertUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ConcertUsers",
                c => new
                    {
                        Concert_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Concert_Id, t.Users_Id });
            
            CreateTable(
                "dbo.TourUsers",
                c => new
                    {
                        Tour_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tour_Id, t.Users_Id });
            
            CreateTable(
                "dbo.UsersUsers",
                c => new
                    {
                        Users_Id = c.Int(nullable: false),
                        Users_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Users_Id, t.Users_Id1 });
            
            AddColumn("dbo.Users", "login", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Band", "password", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Band", "login", c => c.String(nullable: false, maxLength: 15));
            DropForeignKey("dbo.Band", "user_Id", "dbo.Users");
            DropForeignKey("dbo.ConcertFavourites", "Favourites_Id", "dbo.Favourites");
            DropForeignKey("dbo.ConcertFavourites", "Concert_Id", "dbo.Concert");
            DropForeignKey("dbo.TrackGenres", "Genre_Id", "dbo.Genre");
            DropForeignKey("dbo.TrackGenres", "Track_Id", "dbo.Track");
            DropForeignKey("dbo.FavouritesTours", "Tour_Id", "dbo.Tour");
            DropForeignKey("dbo.FavouritesTours", "Favourites_Id", "dbo.Favourites");
            DropForeignKey("dbo.RoleUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropIndex("dbo.ConcertFavourites", new[] { "Favourites_Id" });
            DropIndex("dbo.ConcertFavourites", new[] { "Concert_Id" });
            DropIndex("dbo.TrackGenres", new[] { "Genre_Id" });
            DropIndex("dbo.TrackGenres", new[] { "Track_Id" });
            DropIndex("dbo.FavouritesTours", new[] { "Tour_Id" });
            DropIndex("dbo.FavouritesTours", new[] { "Favourites_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Users_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.Band", new[] { "user_Id" });
            DropColumn("dbo.Track", "rating");
            DropColumn("dbo.Track", "releaseDate");
            DropColumn("dbo.Users", "lastName");
            DropColumn("dbo.Users", "firstName");
            DropColumn("dbo.Band", "user_Id");
            DropColumn("dbo.Band", "bandResolveDate");
            DropColumn("dbo.Band", "bandCreationDate");
            DropColumn("dbo.Band", "addDate");
            DropColumn("dbo.Album", "rating");
            DropTable("dbo.ConcertFavourites");
            DropTable("dbo.TrackGenres");
            DropTable("dbo.FavouritesTours");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.Roles");
            CreateIndex("dbo.ConcertUsers", "Users_Id");
            CreateIndex("dbo.ConcertUsers", "Concert_Id");
            CreateIndex("dbo.TourUsers", "Users_Id");
            CreateIndex("dbo.TourUsers", "Tour_Id");
            CreateIndex("dbo.UsersUsers", "Users_Id1");
            CreateIndex("dbo.UsersUsers", "Users_Id");
            AddForeignKey("dbo.ConcertUsers", "Users_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConcertUsers", "Concert_Id", "dbo.Concert", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TourUsers", "Users_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TourUsers", "Tour_Id", "dbo.Tour", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersUsers", "Users_Id1", "dbo.Users", "Id");
            AddForeignKey("dbo.UsersUsers", "Users_Id", "dbo.Users", "Id");
        }
    }
}
