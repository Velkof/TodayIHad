namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFoodEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "CaloriesKcal", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "ProteinGr", c => c.Single());
            AddColumn("dbo.Foods", "FatGr", c => c.Single());
            AddColumn("dbo.Foods", "CarbsGr", c => c.Single());
            AddColumn("dbo.Foods", "FiberGr", c => c.Single());
            AddColumn("dbo.Foods", "SugarGr", c => c.Single());
            AddColumn("dbo.Foods", "SodiumMg", c => c.Int());
            AddColumn("dbo.Foods", "FatSatGr", c => c.Single());
            AddColumn("dbo.Foods", "FatMonoGr", c => c.Single());
            AddColumn("dbo.Foods", "FatPolyGr", c => c.Single());
            AddColumn("dbo.Foods", "CholesterolMg", c => c.Int());
            DropColumn("dbo.Foods", "Calories_kcal");
            DropColumn("dbo.Foods", "Protein_gr");
            DropColumn("dbo.Foods", "Fat_gr");
            DropColumn("dbo.Foods", "Carbs_gr");
            DropColumn("dbo.Foods", "Fiber_gr");
            DropColumn("dbo.Foods", "Sugar_gr");
            DropColumn("dbo.Foods", "Sodium_mg");
            DropColumn("dbo.Foods", "Fat_Sat_gr");
            DropColumn("dbo.Foods", "Fat_Mono_gr");
            DropColumn("dbo.Foods", "Fat_Poly_gr");
            DropColumn("dbo.Foods", "Cholesterol_mg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "Cholesterol_mg", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Fat_Poly_gr", c => c.Single(nullable: false));
            AddColumn("dbo.Foods", "Fat_Mono_gr", c => c.Single(nullable: false));
            AddColumn("dbo.Foods", "Fat_Sat_gr", c => c.Single(nullable: false));
            AddColumn("dbo.Foods", "Sodium_mg", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Sugar_gr", c => c.Single(nullable: false));
            AddColumn("dbo.Foods", "Fiber_gr", c => c.Single(nullable: false));
            AddColumn("dbo.Foods", "Carbs_gr", c => c.Single(nullable: false));
            AddColumn("dbo.Foods", "Fat_gr", c => c.Single(nullable: false));
            AddColumn("dbo.Foods", "Protein_gr", c => c.Single(nullable: false));
            AddColumn("dbo.Foods", "Calories_kcal", c => c.Int(nullable: false));
            DropColumn("dbo.Foods", "CholesterolMg");
            DropColumn("dbo.Foods", "FatPolyGr");
            DropColumn("dbo.Foods", "FatMonoGr");
            DropColumn("dbo.Foods", "FatSatGr");
            DropColumn("dbo.Foods", "SodiumMg");
            DropColumn("dbo.Foods", "SugarGr");
            DropColumn("dbo.Foods", "FiberGr");
            DropColumn("dbo.Foods", "CarbsGr");
            DropColumn("dbo.Foods", "FatGr");
            DropColumn("dbo.Foods", "ProteinGr");
            DropColumn("dbo.Foods", "CaloriesKcal");
        }
    }
}
