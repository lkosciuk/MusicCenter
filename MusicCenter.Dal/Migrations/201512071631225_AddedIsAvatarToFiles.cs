namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsAvatarToFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "IsAvatar", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "IsAvatar");
        }
    }
}
