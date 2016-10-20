namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationForFoodUnitEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodUnits", "Name", c => c.String(nullable: false, maxLength: 84));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodUnits", "Name", c => c.String(maxLength: 84));
        }
    }
}
