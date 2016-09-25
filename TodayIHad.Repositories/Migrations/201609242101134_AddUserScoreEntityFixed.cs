namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserScoreEntityFixed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Streak = c.Int(nullable: false),
                        ActiveDays = c.Int(nullable: false),
                        Rank = c.Int(nullable: false),
                        ScoreLastUpdated = c.DateTime(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserScores");
        }
    }
}
