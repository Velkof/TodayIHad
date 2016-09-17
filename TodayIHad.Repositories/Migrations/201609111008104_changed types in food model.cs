namespace TodayIHad.Repositories.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class changedtypesinfoodmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "CaloriesKcal", c => c.Single());
            AlterColumn("dbo.Foods", "SodiumMg", c => c.Single());
            AlterColumn("dbo.Foods", "CholesterolMg", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "CholesterolMg", c => c.Int());
            AlterColumn("dbo.Foods", "SodiumMg", c => c.Int());
            AlterColumn("dbo.Foods", "CaloriesKcal", c => c.Int(nullable: false));
        }
    }
}
