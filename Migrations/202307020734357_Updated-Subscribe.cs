namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedSubscribe : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Subscribe", "CreatedBy");
            DropColumn("dbo.tb_Subscribe", "CreatedDate");
            DropColumn("dbo.tb_Subscribe", "ModifiedBy");
            DropColumn("dbo.tb_Subscribe", "ModifiedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Subscribe", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Subscribe", "ModifiedBy", c => c.String());
            AddColumn("dbo.tb_Subscribe", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.tb_Subscribe", "CreatedBy", c => c.String());
        }
    }
}
