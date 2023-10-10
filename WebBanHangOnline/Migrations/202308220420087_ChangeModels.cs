namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Post", "SeoTile", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Post", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.tb_Post", "SeoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Post", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_Post", "SeoDescription", c => c.String());
            AlterColumn("dbo.tb_Post", "SeoTile", c => c.String());
        }
    }
}
