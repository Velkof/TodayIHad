namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserScoreEntity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Score");
            DropColumn("dbo.AspNetUsers", "Level");
            DropColumn("dbo.AspNetUsers", "Streak");
            DropColumn("dbo.AspNetUsers", "ActiveDays");
            DropColumn("dbo.AspNetUsers", "Rank");
            DropColumn("dbo.AspNetUsers", "ScoreLastUpdated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ScoreLastUpdated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Rank", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ActiveDays", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Streak", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Score", c => c.Int(nullable: false));
        }
    }
}
