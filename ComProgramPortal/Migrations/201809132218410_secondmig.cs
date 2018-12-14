namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondmig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.StudentInfoes", "Fullname", c => c.String());
            AddColumn("dbo.TeacherProfiles", "Fullname", c => c.String());
            AddColumn("dbo.TeacherProfiles", "DataStatusChanged", c => c.DateTime());
            AddColumn("dbo.TeacherQualifications", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Packages", "UserId");
            CreateIndex("dbo.TeacherQualifications", "UserId");
            AddForeignKey("dbo.TeacherQualifications", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Packages", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Packages", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "Photo", c => c.Binary());
            DropForeignKey("dbo.Packages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherQualifications", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TeacherQualifications", new[] { "UserId" });
            DropIndex("dbo.Packages", new[] { "UserId" });
            DropColumn("dbo.TeacherQualifications", "UserId");
            DropColumn("dbo.TeacherProfiles", "DataStatusChanged");
            DropColumn("dbo.TeacherProfiles", "Fullname");
            DropColumn("dbo.StudentInfoes", "Fullname");
            DropColumn("dbo.Packages", "UserId");
        }
    }
}
