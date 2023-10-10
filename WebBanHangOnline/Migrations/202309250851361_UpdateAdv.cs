namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAdv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Adv", "DisplayHomeLeft", c => c.Boolean(nullable: false));
            AddColumn("dbo.tb_Adv", "DisplayHomeRight", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Adv", "DisplayHomeRight");
            DropColumn("dbo.tb_Adv", "DisplayHomeLeft");
        }
    }
}
