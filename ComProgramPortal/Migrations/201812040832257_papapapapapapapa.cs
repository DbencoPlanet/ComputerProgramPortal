namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class papapapapapapapa : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Payments", name: "Package_Id", newName: "PackageId");
            RenameIndex(table: "dbo.Payments", name: "IX_Package_Id", newName: "IX_PackageId");
            DropColumn("dbo.Payments", "PackID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "PackID", c => c.Int());
            RenameIndex(table: "dbo.Payments", name: "IX_PackageId", newName: "IX_Package_Id");
            RenameColumn(table: "dbo.Payments", name: "PackageId", newName: "Package_Id");
        }
    }
}
