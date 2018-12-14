namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhdhgsggsgsgs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Assignments", "UserId");
            AddForeignKey("dbo.Assignments", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "UserId" });
            DropColumn("dbo.Assignments", "UserId");
        }
    }
}
