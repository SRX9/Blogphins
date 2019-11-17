namespace tlogNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Microblogs",
                c => new
                    {
                        bid = c.Int(nullable: false, identity: true),
                        uid = c.Int(nullable: false),
                        uname = c.String(nullable: false),
                        time = c.String(nullable: false),
                        text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.bid);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tag = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TagMicroblogs",
                c => new
                    {
                        Tag_id = c.Int(nullable: false),
                        Microblog_bid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_id, t.Microblog_bid })
                .ForeignKey("dbo.Tags", t => t.Tag_id, cascadeDelete: true)
                .ForeignKey("dbo.Microblogs", t => t.Microblog_bid, cascadeDelete: true)
                .Index(t => t.Tag_id)
                .Index(t => t.Microblog_bid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagMicroblogs", "Microblog_bid", "dbo.Microblogs");
            DropForeignKey("dbo.TagMicroblogs", "Tag_id", "dbo.Tags");
            DropIndex("dbo.TagMicroblogs", new[] { "Microblog_bid" });
            DropIndex("dbo.TagMicroblogs", new[] { "Tag_id" });
            DropTable("dbo.TagMicroblogs");
            DropTable("dbo.Tags");
            DropTable("dbo.Microblogs");
        }
    }
}
