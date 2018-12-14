namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mmmmmmmmmmmme : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "TeacherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "TeacherId", c => c.Int(nullable: false));
        }
    }
}
