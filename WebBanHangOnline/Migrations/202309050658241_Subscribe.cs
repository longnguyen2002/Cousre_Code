namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscribe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Subscribe", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Subscribe", "Detail");
        }
    }
}
