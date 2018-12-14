namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eeeeeeeeeeeee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "PackageId", "dbo.Packages");
            DropIndex("dbo.Payments", new[] { "PackageId" });
            CreateTable(
                "dbo.PaymentPackages",
                c => new
                    {
                        Payment_Id = c.Int(nullable: false),
                        Package_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Payment_Id, t.Package_Id })
                .ForeignKey("dbo.Payments", t => t.Payment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.Package_Id, cascadeDelete: true)
                .Index(t => t.Payment_Id)
                .Index(t => t.Package_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentPackages", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.PaymentPackages", "Payment_Id", "dbo.Payments");
            DropIndex("dbo.PaymentPackages", new[] { "Package_Id" });
            DropIndex("dbo.PaymentPackages", new[] { "Payment_Id" });
            DropTable("dbo.PaymentPackages");
            CreateIndex("dbo.Payments", "PackageId");
            AddForeignKey("dbo.Payments", "PackageId", "dbo.Packages", "Id");
        }
    }
}
