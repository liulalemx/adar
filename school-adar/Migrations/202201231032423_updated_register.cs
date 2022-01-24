namespace school_adar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_register : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Housings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LessorID = c.Int(nullable: false),
                        Location = c.String(),
                        Size = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Image = c.Binary(),
                        Features = c.String(),
                        Condition = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lessors", t => t.LessorID, cascadeDelete: true)
                .Index(t => t.LessorID);
            
            CreateTable(
                "dbo.Lessors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNo = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LesseeID = c.Int(nullable: false),
                        HousingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Housings", t => t.HousingID, cascadeDelete: true)
                .ForeignKey("dbo.Lessees", t => t.LesseeID, cascadeDelete: true)
                .Index(t => t.LesseeID)
                .Index(t => t.HousingID);
            
            CreateTable(
                "dbo.Lessees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNo = c.String(),
                        Email = c.String(),
                        Adress = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LesseeID = c.Int(nullable: false),
                        HousingID = c.Int(nullable: false),
                        Rating = c.Double(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Housings", t => t.HousingID, cascadeDelete: true)
                .ForeignKey("dbo.Lessees", t => t.LesseeID, cascadeDelete: true)
                .Index(t => t.LesseeID)
                .Index(t => t.HousingID);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "LesseeID", "dbo.Lessees");
            DropForeignKey("dbo.Reviews", "HousingID", "dbo.Housings");
            DropForeignKey("dbo.Requests", "LesseeID", "dbo.Lessees");
            DropForeignKey("dbo.Requests", "HousingID", "dbo.Housings");
            DropForeignKey("dbo.Housings", "LessorID", "dbo.Lessors");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reviews", new[] { "HousingID" });
            DropIndex("dbo.Reviews", new[] { "LesseeID" });
            DropIndex("dbo.Requests", new[] { "HousingID" });
            DropIndex("dbo.Requests", new[] { "LesseeID" });
            DropIndex("dbo.Housings", new[] { "LessorID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.Lessees");
            DropTable("dbo.Requests");
            DropTable("dbo.Lessors");
            DropTable("dbo.Housings");
        }
    }
}
