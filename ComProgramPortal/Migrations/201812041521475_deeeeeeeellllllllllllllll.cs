namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deeeeeeeellllllllllllllll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentPackages", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.PaymentPackages", "Package_Id", "dbo.Packages");
            DropIndex("dbo.PaymentPackages", new[] { "Payment_Id" });
            DropIndex("dbo.PaymentPackages", new[] { "Package_Id" });
            AddColumn("dbo.Packages", "PaymentId", c => c.Int());
            CreateIndex("dbo.Packages", "PaymentId");
            AddForeignKey("dbo.Packages", "PaymentId", "dbo.Payments", "Id");
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
            
            DropForeignKey("dbo.Packages", "PaymentId", "dbo.Payments");
            DropIndex("dbo.Packages", new[] { "PaymentId" });
            DropColumn("dbo.Packages", "PaymentId");
            CreateIndex("dbo.PaymentPackages", "Package_Id");
            CreateIndex("dbo.PaymentPackages", "Payment_Id");
            AddForeignKey("dbo.PaymentPackages", "Package_Id", "dbo.Packages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PaymentPackages", "Payment_Id", "dbo.Payments", "Id", cascadeDelete: true);
        }
    }
}
