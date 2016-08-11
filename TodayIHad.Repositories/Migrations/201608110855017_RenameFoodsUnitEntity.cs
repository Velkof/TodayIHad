namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFoodsUnitEntity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FoodsUnits", newName: "FoodUnits");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FoodUnits", newName: "FoodsUnits");
        }
    }
}
