namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScoreLastUpdatedUserProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ScoreLastUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ScoreLastUpdated");
        }
    }
}
