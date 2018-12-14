namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meeedeede : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "TeacherId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "TeacherId");
        }
    }
}
