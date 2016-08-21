namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLoggedFoodModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoggedFoods", "ProteinGr", c => c.Single());
            AddColumn("dbo.LoggedFoods", "FatGr", c => c.Single());
            AddColumn("dbo.LoggedFoods", "CarbsGr", c => c.Single());
            AddColumn("dbo.LoggedFoods", "FiberGr", c => c.Single());
            AddColumn("dbo.LoggedFoods", "SugarGr", c => c.Single());
            AddColumn("dbo.LoggedFoods", "SodiumMg", c => c.Int());
            AddColumn("dbo.LoggedFoods", "FatSatGr", c => c.Single());
            AddColumn("dbo.LoggedFoods", "FatMonoGr", c => c.Single());
            AddColumn("dbo.LoggedFoods", "FatPolyGr", c => c.Single());
            AddColumn("dbo.LoggedFoods", "CholesterolMg", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoggedFoods", "CholesterolMg");
            DropColumn("dbo.LoggedFoods", "FatPolyGr");
            DropColumn("dbo.LoggedFoods", "FatMonoGr");
            DropColumn("dbo.LoggedFoods", "FatSatGr");
            DropColumn("dbo.LoggedFoods", "SodiumMg");
            DropColumn("dbo.LoggedFoods", "SugarGr");
            DropColumn("dbo.LoggedFoods", "FiberGr");
            DropColumn("dbo.LoggedFoods", "CarbsGr");
            DropColumn("dbo.LoggedFoods", "FatGr");
            DropColumn("dbo.LoggedFoods", "ProteinGr");
        }
    }
}
