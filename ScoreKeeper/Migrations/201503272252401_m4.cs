namespace ScoreKeeper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Player", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Player", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Player", "ImageName");
            DropColumn("dbo.Player", "IsActive");
        }
    }
}
