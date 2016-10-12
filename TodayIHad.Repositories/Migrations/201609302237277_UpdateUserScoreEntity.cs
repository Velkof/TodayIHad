namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserScoreEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserScores", "UserEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserScores", "UserEmail");
        }
    }
}
