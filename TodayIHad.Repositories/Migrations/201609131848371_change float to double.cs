namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changefloattodouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "CaloriesKcal", c => c.Double());
            AlterColumn("dbo.Foods", "ProteinGr", c => c.Double());
            AlterColumn("dbo.Foods", "FatGr", c => c.Double());
            AlterColumn("dbo.Foods", "CarbsGr", c => c.Double());
            AlterColumn("dbo.Foods", "FiberGr", c => c.Double());
            AlterColumn("dbo.Foods", "SugarGr", c => c.Double());
            AlterColumn("dbo.Foods", "SodiumMg", c => c.Double());
            AlterColumn("dbo.Foods", "FatSatGr", c => c.Double());
            AlterColumn("dbo.Foods", "FatMonoGr", c => c.Double());
            AlterColumn("dbo.Foods", "FatPolyGr", c => c.Double());
            AlterColumn("dbo.Foods", "CholesterolMg", c => c.Double());
            AlterColumn("dbo.FoodUnits", "GramWeight", c => c.Double(nullable: false));
            AlterColumn("dbo.LoggedFoods", "ProteinGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "FatGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "CarbsGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "FiberGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "SugarGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "SodiumMg", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "FatSatGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "FatMonoGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "FatPolyGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "CholesterolMg", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoggedFoods", "CholesterolMg", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "FatPolyGr", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "FatMonoGr", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "FatSatGr", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "SodiumMg", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "SugarGr", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "FiberGr", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "CarbsGr", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "FatGr", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "ProteinGr", c => c.Single());
            AlterColumn("dbo.FoodUnits", "GramWeight", c => c.Single(nullable: false));
            AlterColumn("dbo.Foods", "CholesterolMg", c => c.Single());
            AlterColumn("dbo.Foods", "FatPolyGr", c => c.Single());
            AlterColumn("dbo.Foods", "FatMonoGr", c => c.Single());
            AlterColumn("dbo.Foods", "FatSatGr", c => c.Single());
            AlterColumn("dbo.Foods", "SodiumMg", c => c.Single());
            AlterColumn("dbo.Foods", "SugarGr", c => c.Single());
            AlterColumn("dbo.Foods", "FiberGr", c => c.Single());
            AlterColumn("dbo.Foods", "CarbsGr", c => c.Single());
            AlterColumn("dbo.Foods", "FatGr", c => c.Single());
            AlterColumn("dbo.Foods", "ProteinGr", c => c.Single());
            AlterColumn("dbo.Foods", "CaloriesKcal", c => c.Single());
        }
    }
}
