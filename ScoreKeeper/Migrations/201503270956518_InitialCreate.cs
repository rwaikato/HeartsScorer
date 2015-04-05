namespace ScoreKeeper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        DatePlayed = c.DateTime(nullable: false),
                        MaxScore = c.Int(nullable: false),
                        IsComplete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID);
            
            CreateTable(
                "dbo.PlayersGameMap",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        GameID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Game", t => t.GameID, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID)
                .Index(t => t.GameID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LowestScore = c.Int(nullable: false),
                        HighestScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayersGameMap", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.PlayersGameMap", "GameID", "dbo.Game");
            DropIndex("dbo.PlayersGameMap", new[] { "GameID" });
            DropIndex("dbo.PlayersGameMap", new[] { "PlayerID" });
            DropTable("dbo.Player");
            DropTable("dbo.PlayersGameMap");
            DropTable("dbo.Game");
        }
    }
}
