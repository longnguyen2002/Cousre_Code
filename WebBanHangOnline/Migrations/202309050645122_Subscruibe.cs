namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscruibe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Subscribe", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Subscribe", "Name");
        }
    }
}
