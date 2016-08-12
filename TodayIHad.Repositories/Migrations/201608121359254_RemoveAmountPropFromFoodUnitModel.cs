namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAmountPropFromFoodUnitModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FoodUnits", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodUnits", "Amount", c => c.Single(nullable: false));
        }
    }
}
