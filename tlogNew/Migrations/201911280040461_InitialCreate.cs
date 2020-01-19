namespace tlogNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tag = c.String(),
                        bid = c.Int(nullable: false),
                        uid = c.Int(nullable: false),
                        uname = c.String(),
                        title = c.String(),
                        reads = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Megablogs",
                c => new
                    {
                        bid = c.Int(nullable: false, identity: true),
                        uid = c.Int(nullable: false),
                        uname = c.String(nullable: false),
                        time = c.String(nullable: false),
                        title = c.String(nullable: false),
                        text = c.String(nullable: false),
                        reads = c.Int(nullable: false),
                        category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.bid);
            
            CreateTable(
                "dbo.Microblogs",
                c => new
                    {
                        bid = c.Int(nullable: false, identity: true),
                        uid = c.Int(nullable: false),
                        uname = c.String(),
                        time = c.String(),
                        title = c.String(),
                        text = c.String(),
                        reads = c.Int(nullable: false),
                        tag = c.String(),
                    })
                .PrimaryKey(t => t.bid);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tag = c.String(),
                        bid = c.Int(nullable: false),
                        uid = c.Int(nullable: false),
                        uname = c.String(),
                        title = c.String(),
                        reads = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false),
                        bio = c.String(nullable: false),
                        email = c.String(nullable: false),
                        readers = c.Int(nullable: false),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Tags");
            DropTable("dbo.Microblogs");
            DropTable("dbo.Megablogs");
            DropTable("dbo.Categories");
        }
    }
}
