﻿namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSlideandReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Adv", "TitleReview", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Adv", "TitleReview");
        }
    }
}
