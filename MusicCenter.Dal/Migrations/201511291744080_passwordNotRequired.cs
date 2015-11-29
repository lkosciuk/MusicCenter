namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passwordNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "password", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
