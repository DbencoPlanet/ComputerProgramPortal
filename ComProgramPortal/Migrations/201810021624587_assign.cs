namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assign : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "TeacherProfileID", "dbo.TeacherProfiles");
            DropForeignKey("dbo.AssignmentAnswers", "TeacherProfileID", "dbo.TeacherProfiles");
            DropForeignKey("dbo.AssignmentAnswers", "PackageID", "dbo.Packages");
            DropForeignKey("dbo.AssignmentAnswers", "StudentInfoID", "dbo.StudentInfoes");
            DropForeignKey("dbo.Assignments", "PackageID", "dbo.Packages");
            DropIndex("dbo.AssignmentAnswers", new[] { "TeacherProfileID" });
            DropIndex("dbo.AssignmentAnswers", new[] { "PackageID" });
            DropIndex("dbo.AssignmentAnswers", new[] { "StudentInfoID" });
            DropIndex("dbo.Assignments", new[] { "TeacherProfileID" });
            DropIndex("dbo.Assignments", new[] { "PackageID" });
            RenameColumn(table: "dbo.AssignmentAnswers", name: "StudentInfoID", newName: "StudentInfo_Id");
            RenameColumn(table: "dbo.AssignmentAnswers", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.AssignmentAnswers", name: "IX_User_Id", newName: "IX_UserId");
            AddColumn("dbo.AssignmentAnswers", "DateAnswered", c => c.DateTime(nullable: false));
            AddColumn("dbo.AssignmentAnswers", "DateModified", c => c.DateTime());
            AddColumn("dbo.AssignmentAnswers", "StudentId", c => c.Int());
            AddColumn("dbo.AssignmentAnswers", "EnrollementId", c => c.Int());
            AddColumn("dbo.AssignmentAnswers", "Answer", c => c.String());
            AddColumn("dbo.AssignmentAnswers", "Assessed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Assignments", "Description", c => c.String());
            AddColumn("dbo.Assignments", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Assignments", "DateSubmitionEnds", c => c.DateTime());
            AddColumn("dbo.Assignments", "IsPublished", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AssignmentAnswers", "PackageId", c => c.Int());
            AlterColumn("dbo.AssignmentAnswers", "StudentInfo_Id", c => c.Int());
            AlterColumn("dbo.Assignments", "PackageId", c => c.Int());
            CreateIndex("dbo.AssignmentAnswers", "PackageId");
            CreateIndex("dbo.AssignmentAnswers", "EnrollementId");
            CreateIndex("dbo.AssignmentAnswers", "StudentInfo_Id");
            CreateIndex("dbo.Assignments", "PackageId");
            AddForeignKey("dbo.AssignmentAnswers", "EnrollementId", "dbo.Enrollments", "Id");
            AddForeignKey("dbo.AssignmentAnswers", "PackageId", "dbo.Packages", "Id");
            AddForeignKey("dbo.AssignmentAnswers", "StudentInfo_Id", "dbo.StudentInfoes", "Id");
            AddForeignKey("dbo.Assignments", "PackageId", "dbo.Packages", "Id");
            DropColumn("dbo.AssignmentAnswers", "Title");
            DropColumn("dbo.AssignmentAnswers", "AssignDate");
            DropColumn("dbo.AssignmentAnswers", "SubmitDate");
            DropColumn("dbo.AssignmentAnswers", "TeacherProfileID");
            DropColumn("dbo.Assignments", "AssignDate");
            DropColumn("dbo.Assignments", "SubmitDate");
            DropColumn("dbo.Assignments", "TeacherProfileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assignments", "TeacherProfileID", c => c.Int(nullable: false));
            AddColumn("dbo.Assignments", "SubmitDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Assignments", "AssignDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AssignmentAnswers", "TeacherProfileID", c => c.Int(nullable: false));
            AddColumn("dbo.AssignmentAnswers", "SubmitDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AssignmentAnswers", "AssignDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AssignmentAnswers", "Title", c => c.String());
            DropForeignKey("dbo.Assignments", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.AssignmentAnswers", "StudentInfo_Id", "dbo.StudentInfoes");
            DropForeignKey("dbo.AssignmentAnswers", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.AssignmentAnswers", "EnrollementId", "dbo.Enrollments");
            DropIndex("dbo.Assignments", new[] { "PackageId" });
            DropIndex("dbo.AssignmentAnswers", new[] { "StudentInfo_Id" });
            DropIndex("dbo.AssignmentAnswers", new[] { "EnrollementId" });
            DropIndex("dbo.AssignmentAnswers", new[] { "PackageId" });
            AlterColumn("dbo.Assignments", "PackageId", c => c.Int(nullable: false));
            AlterColumn("dbo.AssignmentAnswers", "StudentInfo_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AssignmentAnswers", "PackageId", c => c.Int(nullable: false));
            DropColumn("dbo.Assignments", "IsPublished");
            DropColumn("dbo.Assignments", "DateSubmitionEnds");
            DropColumn("dbo.Assignments", "DateCreated");
            DropColumn("dbo.Assignments", "Description");
            DropColumn("dbo.AssignmentAnswers", "Assessed");
            DropColumn("dbo.AssignmentAnswers", "Answer");
            DropColumn("dbo.AssignmentAnswers", "EnrollementId");
            DropColumn("dbo.AssignmentAnswers", "StudentId");
            DropColumn("dbo.AssignmentAnswers", "DateModified");
            DropColumn("dbo.AssignmentAnswers", "DateAnswered");
            RenameIndex(table: "dbo.AssignmentAnswers", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.AssignmentAnswers", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.AssignmentAnswers", name: "StudentInfo_Id", newName: "StudentInfoID");
            CreateIndex("dbo.Assignments", "PackageID");
            CreateIndex("dbo.Assignments", "TeacherProfileID");
            CreateIndex("dbo.AssignmentAnswers", "StudentInfoID");
            CreateIndex("dbo.AssignmentAnswers", "PackageID");
            CreateIndex("dbo.AssignmentAnswers", "TeacherProfileID");
            AddForeignKey("dbo.Assignments", "PackageID", "dbo.Packages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AssignmentAnswers", "StudentInfoID", "dbo.StudentInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AssignmentAnswers", "PackageID", "dbo.Packages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AssignmentAnswers", "TeacherProfileID", "dbo.TeacherProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Assignments", "TeacherProfileID", "dbo.TeacherProfiles", "Id", cascadeDelete: true);
        }
    }
}
