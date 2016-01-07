namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsSingle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Track", "IsSingle", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Track", "IsSingle");
        }
    }
}
