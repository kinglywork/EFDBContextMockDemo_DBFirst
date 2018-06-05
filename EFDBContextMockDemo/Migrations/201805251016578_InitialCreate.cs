namespace EFDBContextMockDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Grade_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grades", t => t.Grade_Id)
                .Index(t => t.Grade_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Grade_Id", "dbo.Grades");
            DropIndex("dbo.Students", new[] { "Grade_Id" });
            DropTable("dbo.Students");
            DropTable("dbo.Grades");
        }
    }
}
