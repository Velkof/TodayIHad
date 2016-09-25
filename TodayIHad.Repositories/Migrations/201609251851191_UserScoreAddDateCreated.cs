namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserScoreAddDateCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserScores", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserScores", "DateUpdated", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserScores", "ScoreLastUpdated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserScores", "ScoreLastUpdated", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserScores", "DateUpdated");
            DropColumn("dbo.UserScores", "DateCreated");
        }
    }
}
