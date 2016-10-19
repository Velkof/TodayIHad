namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationToFoodEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Foods", "CaloriesKcal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "CaloriesKcal", c => c.Double());
            AlterColumn("dbo.Foods", "Name", c => c.String(maxLength: 200));
        }
    }
}
