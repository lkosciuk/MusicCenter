namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entitiesFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "password", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "password", c => c.String());
        }
    }
}
