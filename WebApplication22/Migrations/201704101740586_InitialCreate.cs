namespace WebApplication22.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
           /* CreateTable(
                "dbo.drrequests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        request = c.String(nullable: false),
                        status = c.String(),
                        userid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.userid, cascadeDelete: true)
                .Index(t => t.userid);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        userid = c.Int(nullable: false, identity: true),
                        firstname = c.String(nullable: false),
                        lastname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        phone = c.Int(nullable: false),
                        status = c.String(nullable: false),
                        photo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.userid);
            
            CreateTable(
                "dbo.feedbacks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        describtion = c.String(nullable: false),
                        userid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.userid, cascadeDelete: true)
                .Index(t => t.userid);
            
            CreateTable(
                "dbo.projects",
                c => new
                    {
                        projectid = c.Int(nullable: false, identity: true),
                        price = c.Int(nullable: false),
                        describtion = c.String(nullable: false),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.projectid);
            
            CreateTable(
                "dbo.userrequests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        request = c.String(nullable: false),
                        satuts = c.String(),
                        userid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.userid, cascadeDelete: true)
                .Index(t => t.userid);
            
            CreateTable(
                "dbo.worksons",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userid = c.Int(nullable: false),
                        projectid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.projects", t => t.projectid, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.userid, cascadeDelete: true)
                .Index(t => t.userid)
                .Index(t => t.projectid);
            */
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.worksons", "userid", "dbo.users");
            DropForeignKey("dbo.worksons", "projectid", "dbo.projects");
            DropForeignKey("dbo.userrequests", "userid", "dbo.users");
            DropForeignKey("dbo.feedbacks", "userid", "dbo.users");
            DropForeignKey("dbo.drrequests", "userid", "dbo.users");
            DropIndex("dbo.worksons", new[] { "projectid" });
            DropIndex("dbo.worksons", new[] { "userid" });
            DropIndex("dbo.userrequests", new[] { "userid" });
            DropIndex("dbo.feedbacks", new[] { "userid" });
            DropIndex("dbo.drrequests", new[] { "userid" });
            DropTable("dbo.worksons");
            DropTable("dbo.userrequests");
            DropTable("dbo.projects");
            DropTable("dbo.feedbacks");
            DropTable("dbo.users");
            DropTable("dbo.drrequests");
        }
    }
}
