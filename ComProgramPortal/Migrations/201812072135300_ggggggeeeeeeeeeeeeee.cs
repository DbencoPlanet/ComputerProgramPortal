namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggggggeeeeeeeeeeeeee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "PaymentId", "dbo.Payments");
            DropIndex("dbo.Packages", new[] { "PaymentId" });
            AddColumn("dbo.AspNetUsers", "PackageName", c => c.String());
            CreateIndex("dbo.Payments", "PackageId");
            AddForeignKey("dbo.Payments", "PackageId", "dbo.Packages", "Id");
            DropColumn("dbo.Packages", "PaymentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "PaymentId", c => c.Int());
            DropForeignKey("dbo.Payments", "PackageId", "dbo.Packages");
            DropIndex("dbo.Payments", new[] { "PackageId" });
            DropColumn("dbo.AspNetUsers", "PackageName");
            CreateIndex("dbo.Packages", "PaymentId");
            AddForeignKey("dbo.Packages", "PaymentId", "dbo.Payments", "Id");
        }
    }
}
