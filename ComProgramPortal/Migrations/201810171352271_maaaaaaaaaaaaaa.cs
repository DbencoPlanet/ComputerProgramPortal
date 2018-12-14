namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maaaaaaaaaaaaaa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Fullname", c => c.String());
            AddColumn("dbo.Payments", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Payments", "Fullname", c => c.String());
            CreateIndex("dbo.Payments", "UserId");
            AddForeignKey("dbo.Payments", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Packages", "Teacher");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "Teacher", c => c.String());
            DropForeignKey("dbo.Payments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Payments", new[] { "UserId" });
            DropColumn("dbo.Payments", "Fullname");
            DropColumn("dbo.Payments", "UserId");
            DropColumn("dbo.Packages", "Fullname");
        }
    }
}
