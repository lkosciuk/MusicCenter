namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDateBand : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Band", "bandCreationDate", c => c.DateTime());
            AlterColumn("dbo.Band", "bandResolveDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Band", "bandResolveDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Band", "bandCreationDate", c => c.DateTime(nullable: false));
        }
    }
}
