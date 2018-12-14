namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uuuuu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentInfoes", "DateRegistered", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "DateRegistered", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateRegistered", c => c.DateTime(nullable: false));
            AlterColumn("dbo.StudentInfoes", "DateRegistered", c => c.DateTime(nullable: false));
        }
    }
}
