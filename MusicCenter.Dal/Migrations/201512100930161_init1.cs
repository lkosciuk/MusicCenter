namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Favourites", "Id", "dbo.Users");
            DropForeignKey("dbo.Message", "UserID", "dbo.Users");
            DropForeignKey("dbo.MessageUser", "UserID", "dbo.Users");
            DropForeignKey("dbo.RoleUser", "UserID", "dbo.Users");
            DropForeignKey("dbo.Favourites", "user_Id", "dbo.Users");
            DropForeignKey("dbo.Band", "UserID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Id" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Users", "Id");
            AddForeignKey("dbo.Favourites", "Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Message", "UserID", "dbo.Users", "Id");
            AddForeignKey("dbo.MessageUser", "UserID", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoleUser", "UserID", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Favourites", "user_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Band", "UserID", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Band", "UserID", "dbo.Users");
            DropForeignKey("dbo.Favourites", "user_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUser", "UserID", "dbo.Users");
            DropForeignKey("dbo.MessageUser", "UserID", "dbo.Users");
            DropForeignKey("dbo.Message", "UserID", "dbo.Users");
            DropForeignKey("dbo.Favourites", "Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Id" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Users", "Id");
            AddForeignKey("dbo.Band", "UserID", "dbo.Users", "Id");
            AddForeignKey("dbo.Favourites", "user_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.RoleUser", "UserID", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MessageUser", "UserID", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "UserID", "dbo.Users", "Id");
            AddForeignKey("dbo.Favourites", "Id", "dbo.Users", "Id");
        }
    }
}
