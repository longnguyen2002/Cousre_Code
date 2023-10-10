namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_productscategory", "Alias", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.tb_productscategory", "Icon", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_productscategory", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_productscategory", "SeoDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.tb_productscategory", "SeoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_productscategory", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_productscategory", "SeoDescription", c => c.String());
            AlterColumn("dbo.tb_productscategory", "SeoTitle", c => c.String());
            AlterColumn("dbo.tb_productscategory", "Icon", c => c.String());
            DropColumn("dbo.tb_productscategory", "Alias");
        }
    }
}
