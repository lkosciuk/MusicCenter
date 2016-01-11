namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entityMapChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Album", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Album", "duration", c => c.String());
            AlterColumn("dbo.Album", "label", c => c.String());
            AlterColumn("dbo.Album", "producer", c => c.String());
            AlterColumn("dbo.Band", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Band", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Band", "description", c => c.String());
            AlterColumn("dbo.Band", "phoneNumber", c => c.String());
            AlterColumn("dbo.Concert", "address", c => c.String(nullable: false));
            AlterColumn("dbo.Tour", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Tour", "description", c => c.String());
            AlterColumn("dbo.Files", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "path", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "password", c => c.String());
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Message", "title", c => c.String(nullable: false));
            AlterColumn("dbo.Message", "content", c => c.String(nullable: false));
            AlterColumn("dbo.Track", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Track", "duration", c => c.String());
            AlterColumn("dbo.Track", "url", c => c.String());
            AlterColumn("dbo.Genre", "name", c => c.String(nullable: false));
            AlterColumn("dbo.BandMember", "fullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BandMember", "fullName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Genre", "name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Track", "url", c => c.String(maxLength: 200));
            AlterColumn("dbo.Track", "duration", c => c.String(maxLength: 10));
            AlterColumn("dbo.Track", "name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Message", "content", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Message", "title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Users", "password", c => c.String(maxLength: 10));
            AlterColumn("dbo.Files", "path", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Files", "name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tour", "description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tour", "name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Concert", "address", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Band", "phoneNumber", c => c.String(maxLength: 15));
            AlterColumn("dbo.Band", "description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Band", "name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Band", "email", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Album", "producer", c => c.String(maxLength: 50));
            AlterColumn("dbo.Album", "label", c => c.String(maxLength: 50));
            AlterColumn("dbo.Album", "duration", c => c.String(maxLength: 50));
            AlterColumn("dbo.Album", "name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
