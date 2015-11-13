namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Id", "dbo.Favourites");
            DropForeignKey("dbo.Message", "UserAuthor_Id", "dbo.Users");
            DropForeignKey("dbo.MessageUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.Band", "user_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Id" });
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.Users", "favourites_Id", c => c.Int());
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Users", "favourites_Id");
            AddForeignKey("dbo.Users", "favourites_Id", "dbo.Favourites", "Id");
            AddForeignKey("dbo.Message", "UserAuthor_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.MessageUsers", "Users_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RoleUsers", "Users_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Band", "user_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Band", "user_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.MessageUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.Message", "UserAuthor_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "favourites_Id", "dbo.Favourites");
            DropIndex("dbo.Users", new[] { "favourites_Id" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "favourites_Id");
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Users", "Id");
            AddForeignKey("dbo.Band", "user_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.RoleUsers", "Users_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MessageUsers", "Users_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "UserAuthor_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Id", "dbo.Favourites", "Id");
        }
    }
}
