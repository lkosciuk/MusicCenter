namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class albumFieldsNotRequ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Album", "duration", c => c.String(maxLength: 50));
            AlterColumn("dbo.Album", "label", c => c.String(maxLength: 50));
            AlterColumn("dbo.Album", "producer", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Album", "producer", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Album", "label", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Album", "duration", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
