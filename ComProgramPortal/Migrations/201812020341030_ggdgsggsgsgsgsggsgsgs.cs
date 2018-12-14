namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggdgsggsgsgsgsggsgsgs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Image", c => c.Binary());
            AddColumn("dbo.Packages", "ImageId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "ImageId");
            DropColumn("dbo.Packages", "Image");
        }
    }
}
