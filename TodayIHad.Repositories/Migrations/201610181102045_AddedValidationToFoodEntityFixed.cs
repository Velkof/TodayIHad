namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationToFoodEntityFixed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "ProteinGr", c => c.Double(nullable: false));
            AlterColumn("dbo.Foods", "FatGr", c => c.Double(nullable: false));
            AlterColumn("dbo.Foods", "CarbsGr", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "CarbsGr", c => c.Double());
            AlterColumn("dbo.Foods", "FatGr", c => c.Double());
            AlterColumn("dbo.Foods", "ProteinGr", c => c.Double());
        }
    }
}
