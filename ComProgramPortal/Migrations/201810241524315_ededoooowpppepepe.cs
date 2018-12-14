namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ededoooowpppepepe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentInfoes", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.StudentInfoes", "PackageId", "dbo.Packages");
            DropIndex("dbo.StudentInfoes", new[] { "PackageId" });
            DropIndex("dbo.StudentInfoes", new[] { "PaymentId" });
            CreateTable(
                "dbo.StudentInfoPackages",
                c => new
                    {
                        StudentInfo_Id = c.Int(nullable: false),
                        Package_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentInfo_Id, t.Package_Id })
                .ForeignKey("dbo.StudentInfoes", t => t.StudentInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.Package_Id, cascadeDelete: true)
                .Index(t => t.StudentInfo_Id)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.StudentInfoPayments",
                c => new
                    {
                        StudentInfo_Id = c.Int(nullable: false),
                        Payment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentInfo_Id, t.Payment_Id })
                .ForeignKey("dbo.StudentInfoes", t => t.StudentInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Payments", t => t.Payment_Id, cascadeDelete: true)
                .Index(t => t.StudentInfo_Id)
                .Index(t => t.Payment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentInfoPayments", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.StudentInfoPayments", "StudentInfo_Id", "dbo.StudentInfoes");
            DropForeignKey("dbo.StudentInfoPackages", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.StudentInfoPackages", "StudentInfo_Id", "dbo.StudentInfoes");
            DropIndex("dbo.StudentInfoPayments", new[] { "Payment_Id" });
            DropIndex("dbo.StudentInfoPayments", new[] { "StudentInfo_Id" });
            DropIndex("dbo.StudentInfoPackages", new[] { "Package_Id" });
            DropIndex("dbo.StudentInfoPackages", new[] { "StudentInfo_Id" });
            DropTable("dbo.StudentInfoPayments");
            DropTable("dbo.StudentInfoPackages");
            CreateIndex("dbo.StudentInfoes", "PaymentId");
            CreateIndex("dbo.StudentInfoes", "PackageId");
            AddForeignKey("dbo.StudentInfoes", "PackageId", "dbo.Packages", "Id");
            AddForeignKey("dbo.StudentInfoes", "PaymentId", "dbo.Payments", "Id");
        }
    }
}
