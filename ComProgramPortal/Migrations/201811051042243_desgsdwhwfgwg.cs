namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class desgsdwhwfgwg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherProfiles", "StaffRegistrationId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherProfiles", "StaffRegistrationId");
        }
    }
}
