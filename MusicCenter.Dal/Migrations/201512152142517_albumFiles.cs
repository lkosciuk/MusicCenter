namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class albumFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "AlbumID", c => c.Int());
            CreateIndex("dbo.Files", "AlbumID");
            AddForeignKey("dbo.Files", "AlbumID", "dbo.Album", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "AlbumID", "dbo.Album");
            DropIndex("dbo.Files", new[] { "AlbumID" });
            DropColumn("dbo.Files", "AlbumID");
        }
    }
}
