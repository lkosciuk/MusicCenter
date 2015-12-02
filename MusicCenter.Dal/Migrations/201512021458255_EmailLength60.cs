namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailLength60 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
