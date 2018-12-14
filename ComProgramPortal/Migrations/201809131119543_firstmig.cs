namespace ComProgramPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignmentAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AssignDate = c.DateTime(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        TeacherProfileID = c.Int(nullable: false),
                        PackageID = c.Int(nullable: false),
                        StudentInfoID = c.Int(nullable: false),
                        AssignmentId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId)
                .ForeignKey("dbo.Packages", t => t.PackageID, cascadeDelete: true)
                .ForeignKey("dbo.StudentInfoes", t => t.StudentInfoID, cascadeDelete: true)
                .ForeignKey("dbo.TeacherProfiles", t => t.TeacherProfileID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.TeacherProfileID)
                .Index(t => t.PackageID)
                .Index(t => t.StudentInfoID)
                .Index(t => t.AssignmentId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AssignDate = c.DateTime(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        TeacherProfileID = c.Int(nullable: false),
                        PackageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.PackageID, cascadeDelete: true)
                .ForeignKey("dbo.TeacherProfiles", t => t.TeacherProfileID, cascadeDelete: true)
                .Index(t => t.TeacherProfileID)
                .Index(t => t.PackageID);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageName = c.String(nullable: false, maxLength: 200),
                        Duration = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PackageAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackageInfo = c.String(),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageID = c.Int(nullable: false),
                        StudentInfoID = c.Int(nullable: false),
                        PaymentID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.PackageID, cascadeDelete: true)
                .ForeignKey("dbo.Payments", t => t.PaymentID, cascadeDelete: true)
                .ForeignKey("dbo.StudentInfoes", t => t.StudentInfoID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.PackageID)
                .Index(t => t.StudentInfoID)
                .Index(t => t.PaymentID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                        StudentName = c.String(),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Surname = c.String(),
                        Firstname = c.String(),
                        Othername = c.String(),
                        Email = c.String(),
                        Username = c.String(),
                        DateOfBirth = c.DateTime(),
                        DateRegistered = c.DateTime(nullable: false),
                        DataStatusChanged = c.DateTime(),
                        Religion = c.String(),
                        Gender = c.String(),
                        ContactAddress = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        LocalGov = c.String(),
                        StateOfOrigin = c.String(),
                        AboutMe = c.String(),
                        FavouriteFood = c.String(),
                        BooksYouLike = c.String(),
                        MoviesYouLike = c.String(),
                        TVShowsYouLike = c.String(),
                        YourLikes = c.String(),
                        YourDisLikes = c.String(),
                        FavouriteColour = c.String(),
                        PreviousCompKnowledge = c.String(),
                        ParentGuardianName = c.String(),
                        ParentGuardianAddress = c.String(),
                        ParentGuardianPhoneNumber = c.String(),
                        ParentGuardianOccupation = c.String(),
                        Nationality = c.String(),
                        Disability = c.String(),
                        EmergencyContact = c.String(),
                        StudentRegNumber = c.String(),
                        RegisteredBy = c.String(),
                        Image = c.Binary(),
                        ImageId = c.Int(nullable: false),
                        Payment_Id = c.Int(),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Payments", t => t.Payment_Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.UserId)
                .Index(t => t.Payment_Id)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Surname = c.String(),
                        FirstName = c.String(),
                        OtherName = c.String(),
                        DateOfBirth = c.DateTime(),
                        DateRegistered = c.DateTime(nullable: false),
                        DataStatusChanged = c.DateTime(),
                        Religion = c.String(),
                        Gender = c.String(),
                        ContactAddress = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        LocalGov = c.String(),
                        StateOfOrigin = c.String(),
                        Nationality = c.String(),
                        RegisteredBy = c.String(),
                        IsLocked = c.Boolean(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.TeacherProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Disability = c.String(),
                        EmergencyContact = c.String(),
                        MaritalStatus = c.String(),
                        DateOfAppointment = c.DateTime(nullable: false),
                        AboutMe = c.String(),
                        FavouriteFood = c.String(),
                        BooksYouLike = c.String(),
                        MoviesYouLike = c.String(),
                        TVShowsYouLike = c.String(),
                        YourLikes = c.String(),
                        YourDisLikes = c.String(),
                        FavouriteColour = c.String(),
                        Surname = c.String(),
                        Firstname = c.String(),
                        Othername = c.String(),
                        Email = c.String(),
                        Username = c.String(),
                        DateOfBirth = c.DateTime(),
                        DateRegistered = c.DateTime(nullable: false),
                        Religion = c.String(),
                        Gender = c.String(),
                        ContactAddress = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        LocalGov = c.String(),
                        StateOfOrigin = c.String(),
                        Nationality = c.String(),
                        RegisteredBy = c.String(),
                        Image = c.Binary(),
                        ImageId = c.Int(nullable: false),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.UserId)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.TeacherQualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOfInstitution = c.String(nullable: false),
                        YearObtained = c.String(nullable: false),
                        CertificateObtained = c.String(nullable: false),
                        TeacherProfileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeacherProfiles", t => t.TeacherProfileID, cascadeDelete: true)
                .Index(t => t.TeacherProfileID);
            
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        ImageContent = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocalGovs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LGAName = c.String(),
                        StatesId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StatesId)
                .Index(t => t.StatesId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StudentInfoDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(),
                        Surname = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        OtherNames = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        LastPrimarySchoolAttended = c.String(nullable: false),
                        Religion = c.String(nullable: false),
                        NameOfParents = c.String(nullable: false),
                        ParentsAddress = c.String(nullable: false),
                        PhoneNumberOfParents = c.String(nullable: false),
                        OccupationOfParents = c.String(nullable: false),
                        PermanentHomeAddress = c.String(nullable: false),
                        LocalGov = c.String(),
                        StateOfOrigin = c.String(),
                        Nationality = c.String(nullable: false),
                        Disability = c.String(),
                        EmergencyContact = c.String(nullable: false),
                        ApplicationDate = c.DateTime(),
                        ImageId = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentPackages",
                c => new
                    {
                        Payment_Id = c.Int(nullable: false),
                        Package_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Payment_Id, t.Package_Id })
                .ForeignKey("dbo.Payments", t => t.Payment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.Package_Id, cascadeDelete: true)
                .Index(t => t.Payment_Id)
                .Index(t => t.Package_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LocalGovs", "StatesId", "dbo.States");
            DropForeignKey("dbo.AssignmentAnswers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignmentAnswers", "TeacherProfileID", "dbo.TeacherProfiles");
            DropForeignKey("dbo.AssignmentAnswers", "StudentInfoID", "dbo.StudentInfoes");
            DropForeignKey("dbo.AssignmentAnswers", "PackageID", "dbo.Packages");
            DropForeignKey("dbo.Assignments", "TeacherProfileID", "dbo.TeacherProfiles");
            DropForeignKey("dbo.Assignments", "PackageID", "dbo.Packages");
            DropForeignKey("dbo.TeacherProfiles", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.TeacherProfiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherQualifications", "TeacherProfileID", "dbo.TeacherProfiles");
            DropForeignKey("dbo.StudentInfoes", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.Files", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.Enrollments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrollments", "StudentInfoID", "dbo.StudentInfoes");
            DropForeignKey("dbo.StudentInfoes", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.StudentInfoes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentPackages", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.PaymentPackages", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.Enrollments", "PaymentID", "dbo.Payments");
            DropForeignKey("dbo.Enrollments", "PackageID", "dbo.Packages");
            DropForeignKey("dbo.AssignmentAnswers", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.PaymentPackages", new[] { "Package_Id" });
            DropIndex("dbo.PaymentPackages", new[] { "Payment_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LocalGovs", new[] { "StatesId" });
            DropIndex("dbo.TeacherQualifications", new[] { "TeacherProfileID" });
            DropIndex("dbo.TeacherProfiles", new[] { "Package_Id" });
            DropIndex("dbo.TeacherProfiles", new[] { "UserId" });
            DropIndex("dbo.Files", new[] { "Package_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.StudentInfoes", new[] { "Package_Id" });
            DropIndex("dbo.StudentInfoes", new[] { "Payment_Id" });
            DropIndex("dbo.StudentInfoes", new[] { "UserId" });
            DropIndex("dbo.Enrollments", new[] { "User_Id" });
            DropIndex("dbo.Enrollments", new[] { "PaymentID" });
            DropIndex("dbo.Enrollments", new[] { "StudentInfoID" });
            DropIndex("dbo.Enrollments", new[] { "PackageID" });
            DropIndex("dbo.Assignments", new[] { "PackageID" });
            DropIndex("dbo.Assignments", new[] { "TeacherProfileID" });
            DropIndex("dbo.AssignmentAnswers", new[] { "User_Id" });
            DropIndex("dbo.AssignmentAnswers", new[] { "AssignmentId" });
            DropIndex("dbo.AssignmentAnswers", new[] { "StudentInfoID" });
            DropIndex("dbo.AssignmentAnswers", new[] { "PackageID" });
            DropIndex("dbo.AssignmentAnswers", new[] { "TeacherProfileID" });
            DropTable("dbo.PaymentPackages");
            DropTable("dbo.StudentInfoDatas");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.States");
            DropTable("dbo.LocalGovs");
            DropTable("dbo.ImageModels");
            DropTable("dbo.TeacherQualifications");
            DropTable("dbo.TeacherProfiles");
            DropTable("dbo.Files");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StudentInfoes");
            DropTable("dbo.Payments");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Packages");
            DropTable("dbo.Assignments");
            DropTable("dbo.AssignmentAnswers");
        }
    }
}
