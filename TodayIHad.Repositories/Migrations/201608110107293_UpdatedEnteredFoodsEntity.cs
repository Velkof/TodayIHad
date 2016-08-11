namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEnteredFoodsEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnteredFoods", "Calories", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EnteredFoods", "Calories");
        }
    }
}
