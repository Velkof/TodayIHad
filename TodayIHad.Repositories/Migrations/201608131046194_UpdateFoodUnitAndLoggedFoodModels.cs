namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFoodUnitAndLoggedFoodModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FoodUnits", "FoodId", "dbo.Foods");
            DropForeignKey("dbo.LoggedFoods", "FoodId", "dbo.Foods");
            DropIndex("dbo.FoodUnits", new[] { "FoodId" });
            DropIndex("dbo.LoggedFoods", new[] { "FoodId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.LoggedFoods", "FoodId");
            CreateIndex("dbo.FoodUnits", "FoodId");
            AddForeignKey("dbo.LoggedFoods", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FoodUnits", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
        }
    }
}
