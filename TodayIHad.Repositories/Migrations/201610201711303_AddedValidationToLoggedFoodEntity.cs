namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationToLoggedFoodEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "Calories", c => c.Double(nullable: false));
            AlterColumn("dbo.LoggedFoods", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.LoggedFoods", "Unit", c => c.String(nullable: false, maxLength: 84));
            AlterColumn("dbo.LoggedFoods", "Calories", c => c.Double(nullable: false));
            AlterColumn("dbo.LoggedFoods", "ProteinGr", c => c.Double(nullable: false));
            AlterColumn("dbo.LoggedFoods", "FatGr", c => c.Double(nullable: false));
            AlterColumn("dbo.LoggedFoods", "CarbsGr", c => c.Double(nullable: false));
            DropColumn("dbo.Foods", "CaloriesKcal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "CaloriesKcal", c => c.Double(nullable: false));
            AlterColumn("dbo.LoggedFoods", "CarbsGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "FatGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "ProteinGr", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "Calories", c => c.Double());
            AlterColumn("dbo.LoggedFoods", "Unit", c => c.String(maxLength: 84));
            AlterColumn("dbo.LoggedFoods", "Name", c => c.String(maxLength: 200));
            DropColumn("dbo.Foods", "Calories");
        }
    }
}
