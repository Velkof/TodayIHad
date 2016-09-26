namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserScoreUpdateEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserScores", "ScoreToNextLevel", c => c.Int(nullable: false));
            AddColumn("dbo.UserScores", "PercentOfLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserScores", "PercentOfLevel");
            DropColumn("dbo.UserScores", "ScoreToNextLevel");
        }
    }
}
