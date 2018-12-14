namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ponki : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "PackageId", "dbo.Packages");
            DropIndex("dbo.Assignments", new[] { "PackageId" });
            AlterColumn("dbo.Assignments", "PackageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Assignments", "PackageId");
            AddForeignKey("dbo.Assignments", "PackageId", "dbo.Packages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "PackageId", "dbo.Packages");
            DropIndex("dbo.Assignments", new[] { "PackageId" });
            AlterColumn("dbo.Assignments", "PackageId", c => c.Int());
            CreateIndex("dbo.Assignments", "PackageId");
            AddForeignKey("dbo.Assignments", "PackageId", "dbo.Packages", "Id");
        }
    }
}
