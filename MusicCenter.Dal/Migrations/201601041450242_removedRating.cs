namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedRating : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Album", "rating");
            DropColumn("dbo.Track", "rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Track", "rating", c => c.Int(nullable: false));
            AddColumn("dbo.Album", "rating", c => c.Int(nullable: false));
        }
    }
}
