namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserScoreEntity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserScores", "SevenDayScore", c => c.Int(nullable: false));
            AddColumn("dbo.UserScores", "SevenDayScoreLastReset", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserScores", "UserName", c => c.String());
            DropColumn("dbo.UserScores", "NextLevel");
            DropColumn("dbo.UserScores", "ScoreToNextLevel");
            DropColumn("dbo.UserScores", "PercentOfLevel");
            DropColumn("dbo.UserScores", "Rank");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserScores", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.UserScores", "PercentOfLevel", c => c.Int(nullable: false));
            AddColumn("dbo.UserScores", "ScoreToNextLevel", c => c.Int(nullable: false));
            AddColumn("dbo.UserScores", "NextLevel", c => c.Int(nullable: false));
            DropColumn("dbo.UserScores", "UserName");
            DropColumn("dbo.UserScores", "SevenDayScoreLastReset");
            DropColumn("dbo.UserScores", "SevenDayScore");
        }
    }
}
