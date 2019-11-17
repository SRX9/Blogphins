namespace tlogNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagMicroblogs", "Tag_id", "dbo.Tags");
            DropForeignKey("dbo.TagMicroblogs", "Microblog_bid", "dbo.Microblogs");
            DropIndex("dbo.TagMicroblogs", new[] { "Tag_id" });
            DropIndex("dbo.TagMicroblogs", new[] { "Microblog_bid" });
            AddColumn("dbo.Microblogs", "title", c => c.String(nullable: false));
            AddColumn("dbo.Microblogs", "reads", c => c.Int(nullable: false));
            AddColumn("dbo.Microblogs", "tag", c => c.String(nullable: false));
            AddColumn("dbo.Users", "readers", c => c.Int(nullable: false));
            DropTable("dbo.Tags");
            DropTable("dbo.TagMicroblogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagMicroblogs",
                c => new
                    {
                        Tag_id = c.Int(nullable: false),
                        Microblog_bid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_id, t.Microblog_bid });
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tag = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            DropColumn("dbo.Users", "readers");
            DropColumn("dbo.Microblogs", "tag");
            DropColumn("dbo.Microblogs", "reads");
            DropColumn("dbo.Microblogs", "title");
            CreateIndex("dbo.TagMicroblogs", "Microblog_bid");
            CreateIndex("dbo.TagMicroblogs", "Tag_id");
            AddForeignKey("dbo.TagMicroblogs", "Microblog_bid", "dbo.Microblogs", "bid", cascadeDelete: true);
            AddForeignKey("dbo.TagMicroblogs", "Tag_id", "dbo.Tags", "id", cascadeDelete: true);
        }
    }
}
