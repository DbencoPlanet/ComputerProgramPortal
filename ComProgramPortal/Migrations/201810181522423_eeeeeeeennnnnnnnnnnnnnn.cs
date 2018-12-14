namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eeeeeeeennnnnnnnnnnnnnn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentPackages", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.PaymentPackages", "Package_Id", "dbo.Packages");
            DropIndex("dbo.PaymentPackages", new[] { "Payment_Id" });
            DropIndex("dbo.PaymentPackages", new[] { "Package_Id" });
            AddColumn("dbo.Packages", "Payment_Id", c => c.Int());
            AddColumn("dbo.Payments", "PackID", c => c.Int());
            AddColumn("dbo.Payments", "Package_Id", c => c.Int());
            AddColumn("dbo.Payments", "Package_Id1", c => c.Int());
            CreateIndex("dbo.Packages", "Payment_Id");
            CreateIndex("dbo.Payments", "Package_Id");
            CreateIndex("dbo.Payments", "Package_Id1");
            AddForeignKey("dbo.Payments", "Package_Id", "dbo.Packages", "Id");
            AddForeignKey("dbo.Packages", "Payment_Id", "dbo.Payments", "Id");
            AddForeignKey("dbo.Payments", "Package_Id1", "dbo.Packages", "Id");
            DropColumn("dbo.Payments", "PackageName");
            DropTable("dbo.PaymentPackages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentPackages",
                c => new
                    {
                        Payment_Id = c.Int(nullable: false),
                        Package_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Payment_Id, t.Package_Id });
            
            AddColumn("dbo.Payments", "PackageName", c => c.String());
            DropForeignKey("dbo.Payments", "Package_Id1", "dbo.Packages");
            DropForeignKey("dbo.Packages", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.Payments", "Package_Id", "dbo.Packages");
            DropIndex("dbo.Payments", new[] { "Package_Id1" });
            DropIndex("dbo.Payments", new[] { "Package_Id" });
            DropIndex("dbo.Packages", new[] { "Payment_Id" });
            DropColumn("dbo.Payments", "Package_Id1");
            DropColumn("dbo.Payments", "Package_Id");
            DropColumn("dbo.Payments", "PackID");
            DropColumn("dbo.Packages", "Payment_Id");
            CreateIndex("dbo.PaymentPackages", "Package_Id");
            CreateIndex("dbo.PaymentPackages", "Payment_Id");
            AddForeignKey("dbo.PaymentPackages", "Package_Id", "dbo.Packages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PaymentPackages", "Payment_Id", "dbo.Payments", "Id", cascadeDelete: true);
        }
    }
}
