namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserScoreAddNextLevelProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserScores", "NextLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserScores", "NextLevel");
        }
    }
}
