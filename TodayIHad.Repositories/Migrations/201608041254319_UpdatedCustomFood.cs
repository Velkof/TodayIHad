namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedCustomFood : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Foods", newName: "CustomFoods");
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Calories = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.CustomFoods", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomFoods", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Foods");
            RenameTable(name: "dbo.CustomFoods", newName: "Foods");
        }
    }
}
