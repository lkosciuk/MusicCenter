namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "profilePhoto_Id", "dbo.Files");
            DropForeignKey("dbo.Users", "Id", "dbo.Files");
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "profilePhoto_Id" });
            AddColumn("dbo.Files", "UserID", c => c.Int());
            CreateIndex("dbo.Files", "UserID");
            AddForeignKey("dbo.Files", "UserID", "dbo.Users", "Id");
            DropColumn("dbo.Users", "profilePhoto_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "profilePhoto_Id", c => c.Int());
            DropForeignKey("dbo.Files", "UserID", "dbo.Users");
            DropIndex("dbo.Files", new[] { "UserID" });
            DropColumn("dbo.Files", "UserID");
            CreateIndex("dbo.Users", "profilePhoto_Id");
            CreateIndex("dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Id", "dbo.Files", "Id");
            AddForeignKey("dbo.Users", "profilePhoto_Id", "dbo.Files", "Id");
        }
    }
}
