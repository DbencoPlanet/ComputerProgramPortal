namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deeee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TeacherProfiles", "DateOfAppointment", c => c.DateTime());
            AlterColumn("dbo.TeacherProfiles", "DateRegistered", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeacherProfiles", "DateRegistered", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TeacherProfiles", "DateOfAppointment", c => c.DateTime(nullable: false));
        }
    }
}
