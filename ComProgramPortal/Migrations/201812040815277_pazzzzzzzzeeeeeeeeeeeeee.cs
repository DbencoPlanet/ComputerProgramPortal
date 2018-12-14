namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pazzzzzzzzeeeeeeeeeeeeee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.Payments", "Package_Id1", "dbo.Packages");
            DropIndex("dbo.Packages", new[] { "Payment_Id" });
            DropIndex("dbo.Payments", new[] { "Package_Id1" });
            DropColumn("dbo.Packages", "Payment_Id");
            DropColumn("dbo.Payments", "StudentName");
            DropColumn("dbo.Payments", "Package_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "Package_Id1", c => c.Int());
            AddColumn("dbo.Payments", "StudentName", c => c.String());
            AddColumn("dbo.Packages", "Payment_Id", c => c.Int());
            CreateIndex("dbo.Payments", "Package_Id1");
            CreateIndex("dbo.Packages", "Payment_Id");
            AddForeignKey("dbo.Payments", "Package_Id1", "dbo.Packages", "Id");
            AddForeignKey("dbo.Packages", "Payment_Id", "dbo.Payments", "Id");
        }
    }
}
