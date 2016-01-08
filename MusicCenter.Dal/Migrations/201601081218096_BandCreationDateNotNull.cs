namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BandCreationDateNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Band", "bandCreationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Band", "bandCreationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
    }
}
