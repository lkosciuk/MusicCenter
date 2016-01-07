namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedUnusedFromConcert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Concert", "BandID", "dbo.Band");
            DropIndex("dbo.Concert", new[] { "BandID" });
            CreateTable(
                "dbo.BandConcert",
                c => new
                    {
                        BandID = c.Int(nullable: false),
                        ConcertID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BandID, t.ConcertID })
                .ForeignKey("dbo.Concert", t => t.BandID, cascadeDelete: true)
                .ForeignKey("dbo.Band", t => t.ConcertID, cascadeDelete: true)
                .Index(t => t.BandID)
                .Index(t => t.ConcertID);
            
            AddColumn("dbo.Concert", "description", c => c.String());
            DropColumn("dbo.Concert", "ticketPrice");
            DropColumn("dbo.Concert", "ticketUrl");
            DropColumn("dbo.Concert", "BandID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Concert", "BandID", c => c.Int(nullable: false));
            AddColumn("dbo.Concert", "ticketUrl", c => c.String(maxLength: 200));
            AddColumn("dbo.Concert", "ticketPrice", c => c.String(maxLength: 15));
            DropForeignKey("dbo.BandConcert", "ConcertID", "dbo.Band");
            DropForeignKey("dbo.BandConcert", "BandID", "dbo.Concert");
            DropIndex("dbo.BandConcert", new[] { "ConcertID" });
            DropIndex("dbo.BandConcert", new[] { "BandID" });
            DropColumn("dbo.Concert", "description");
            DropTable("dbo.BandConcert");
            CreateIndex("dbo.Concert", "BandID");
            AddForeignKey("dbo.Concert", "BandID", "dbo.Band", "Id");
        }
    }
}
