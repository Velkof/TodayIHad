namespace TodayIHad.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedfoodforeignkeyfromuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foods", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Foods", new[] { "User_Id" });
            DropColumn("dbo.Foods", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Foods", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Foods", "User_Id");
            AddForeignKey("dbo.Foods", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
