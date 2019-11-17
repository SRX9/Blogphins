namespace tlogNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Udatelatest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Microblogs", "uname", c => c.String());
            AlterColumn("dbo.Microblogs", "time", c => c.String());
            AlterColumn("dbo.Microblogs", "title", c => c.String());
            AlterColumn("dbo.Microblogs", "text", c => c.String());
            AlterColumn("dbo.Microblogs", "tag", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Microblogs", "tag", c => c.String(nullable: false));
            AlterColumn("dbo.Microblogs", "text", c => c.String(nullable: false));
            AlterColumn("dbo.Microblogs", "title", c => c.String(nullable: false));
            AlterColumn("dbo.Microblogs", "time", c => c.String(nullable: false));
            AlterColumn("dbo.Microblogs", "uname", c => c.String(nullable: false));
        }
    }
}
