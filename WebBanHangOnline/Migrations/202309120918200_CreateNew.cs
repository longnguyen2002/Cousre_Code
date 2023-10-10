namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_New", "IsNew", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_New", "IsNew");
        }
    }
}
