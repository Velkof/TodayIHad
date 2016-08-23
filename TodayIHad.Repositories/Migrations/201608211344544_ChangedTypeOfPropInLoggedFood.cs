namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTypeOfPropInLoggedFood : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoggedFoods", "SodiumMg", c => c.Single());
            AlterColumn("dbo.LoggedFoods", "CholesterolMg", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoggedFoods", "CholesterolMg", c => c.Int());
            AlterColumn("dbo.LoggedFoods", "SodiumMg", c => c.Int());
        }
    }
}
