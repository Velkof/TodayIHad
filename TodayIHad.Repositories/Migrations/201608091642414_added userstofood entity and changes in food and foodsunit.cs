namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduserstofoodentityandchangesinfoodandfoodsunit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FoodsUnits", "Food_Id", "dbo.Foods");
            DropIndex("dbo.FoodsUnits", new[] { "Food_Id" });
            RenameColumn(table: "dbo.FoodsUnits", name: "Food_Id", newName: "FoodId");
            AddColumn("dbo.Foods", "IsDefault", c => c.Int(nullable: false));
            AlterColumn("dbo.FoodsUnits", "FoodId", c => c.Int(nullable: false));
            CreateIndex("dbo.FoodsUnits", "FoodId");
            AddForeignKey("dbo.FoodsUnits", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
            DropColumn("dbo.FoodsUnits", "UserFoodId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodsUnits", "UserFoodId", c => c.Int(nullable: false));
            DropForeignKey("dbo.FoodsUnits", "FoodId", "dbo.Foods");
            DropIndex("dbo.FoodsUnits", new[] { "FoodId" });
            AlterColumn("dbo.FoodsUnits", "FoodId", c => c.Int());
            DropColumn("dbo.Foods", "IsDefault");
            RenameColumn(table: "dbo.FoodsUnits", name: "FoodId", newName: "Food_Id");
            CreateIndex("dbo.FoodsUnits", "Food_Id");
            AddForeignKey("dbo.FoodsUnits", "Food_Id", "dbo.Foods", "Id");
        }
    }
}
