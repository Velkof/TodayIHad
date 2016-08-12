namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxLengthProperties : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EnteredFoods", newName: "LoggedFoods");
            AlterColumn("dbo.LoggedFoods", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.LoggedFoods", "Unit", c => c.String(maxLength: 84));
            AlterColumn("dbo.Foods", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.FoodUnits", "Name", c => c.String(maxLength: 84));
            CreateIndex("dbo.LoggedFoods", "FoodId");
            AddForeignKey("dbo.LoggedFoods", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
            DropColumn("dbo.FoodUnits", "Seq");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodUnits", "Seq", c => c.String());
            DropForeignKey("dbo.LoggedFoods", "FoodId", "dbo.Foods");
            DropIndex("dbo.LoggedFoods", new[] { "FoodId" });
            AlterColumn("dbo.FoodUnits", "Name", c => c.String());
            AlterColumn("dbo.Foods", "Name", c => c.String());
            AlterColumn("dbo.LoggedFoods", "Unit", c => c.String());
            AlterColumn("dbo.LoggedFoods", "Name", c => c.String());
            RenameTable(name: "dbo.LoggedFoods", newName: "EnteredFoods");
        }
    }
}
