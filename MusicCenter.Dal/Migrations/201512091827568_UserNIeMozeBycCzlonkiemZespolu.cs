namespace MusicCenter.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNIeMozeBycCzlonkiemZespolu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BandMember", "user_Id", "dbo.Users");
            DropForeignKey("dbo.BandMember", "Id", "dbo.Users");
            DropForeignKey("dbo.BandToBandMember", "MemberID", "dbo.BandMember");
            DropIndex("dbo.BandMember", new[] { "Id" });
            DropIndex("dbo.BandMember", new[] { "user_Id" });
            DropPrimaryKey("dbo.BandMember");
            AlterColumn("dbo.BandMember", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BandMember", "Id");
            AddForeignKey("dbo.BandToBandMember", "MemberID", "dbo.BandMember", "Id", cascadeDelete: true);
            DropColumn("dbo.BandMember", "user_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BandMember", "user_Id", c => c.Int());
            DropForeignKey("dbo.BandToBandMember", "MemberID", "dbo.BandMember");
            DropPrimaryKey("dbo.BandMember");
            AlterColumn("dbo.BandMember", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.BandMember", "Id");
            CreateIndex("dbo.BandMember", "user_Id");
            CreateIndex("dbo.BandMember", "Id");
            AddForeignKey("dbo.BandToBandMember", "MemberID", "dbo.BandMember", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BandMember", "Id", "dbo.Users", "Id");
            AddForeignKey("dbo.BandMember", "user_Id", "dbo.Users", "Id");
        }
    }
}
