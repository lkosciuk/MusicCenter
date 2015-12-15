namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bandDate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Band", "bandCreationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Band", "bandResolveDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Band", "bandResolveDate", c => c.DateTime());
            AlterColumn("dbo.Band", "bandCreationDate", c => c.DateTime());
        }
    }
}
