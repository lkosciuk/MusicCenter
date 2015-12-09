namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class niedziala : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Files", new[] { "Id" });
            DropIndex("dbo.Files", new[] { "user_Id" });
            DropColumn("dbo.Users", "Id");
            RenameColumn(table: "dbo.Users", name: "user_Id", newName: "Id");
            CreateIndex("dbo.Users", "Id");
            DropColumn("dbo.Files", "user_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "user_Id", c => c.Int());
            DropIndex("dbo.Users", new[] { "Id" });
            RenameColumn(table: "dbo.Users", name: "Id", newName: "user_Id");
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Files", "user_Id");
            CreateIndex("dbo.Files", "Id");
        }
    }
}
