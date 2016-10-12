namespace TodayIHad.Repositories.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFollowersToFollowedEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowersToFollowed",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FollowerId = c.String(),
                        FollowedId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FollowersToFollowed");
        }
    }
}
