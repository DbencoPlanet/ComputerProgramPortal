namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eeeeeeeeeeeeeeeeeeeeerrrrrrrrrrrr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Enrollments", "PackageID", "dbo.Packages");
            DropForeignKey("dbo.Enrollments", "PaymentID", "dbo.Payments");
            DropIndex("dbo.Assignments", new[] { "PackageId" });
            DropIndex("dbo.Enrollments", new[] { "PackageID" });
            DropIndex("dbo.Enrollments", new[] { "PaymentID" });
            RenameColumn(table: "dbo.Enrollments", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Enrollments", name: "IX_User_Id", newName: "IX_UserId");
            AlterColumn("dbo.Assignments", "PackageId", c => c.Int());
            AlterColumn("dbo.Enrollments", "PackageID", c => c.Int());
            AlterColumn("dbo.Enrollments", "PaymentID", c => c.Int());
            CreateIndex("dbo.Assignments", "PackageId");
            CreateIndex("dbo.Enrollments", "PackageID");
            CreateIndex("dbo.Enrollments", "PaymentID");
            AddForeignKey("dbo.Assignments", "PackageId", "dbo.Packages", "Id");
            AddForeignKey("dbo.Enrollments", "PackageID", "dbo.Packages", "Id");
            AddForeignKey("dbo.Enrollments", "PaymentID", "dbo.Payments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "PaymentID", "dbo.Payments");
            DropForeignKey("dbo.Enrollments", "PackageID", "dbo.Packages");
            DropForeignKey("dbo.Assignments", "PackageId", "dbo.Packages");
            DropIndex("dbo.Enrollments", new[] { "PaymentID" });
            DropIndex("dbo.Enrollments", new[] { "PackageID" });
            DropIndex("dbo.Assignments", new[] { "PackageId" });
            AlterColumn("dbo.Enrollments", "PaymentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Enrollments", "PackageID", c => c.Int(nullable: false));
            AlterColumn("dbo.Assignments", "PackageId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Enrollments", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Enrollments", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Enrollments", "PaymentID");
            CreateIndex("dbo.Enrollments", "PackageID");
            CreateIndex("dbo.Assignments", "PackageId");
            AddForeignKey("dbo.Enrollments", "PaymentID", "dbo.Payments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Enrollments", "PackageID", "dbo.Packages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Assignments", "PackageId", "dbo.Packages", "Id", cascadeDelete: true);
        }
    }
}
