namespace tlogNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModelsnew : DbMigration
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
                "dbo.Saves",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        uid = c.Int(nullable: false),
                        bid = c.Int(nullable: false),
                        title = c.String(),
                        uname = c.String(),
                        type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tags");
            DropTable("dbo.Saves");
            DropTable("dbo.Megablogs");
            DropTable("dbo.Categories");
        }
    }
}
