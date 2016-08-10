using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Repositories
{
    public class Database : IdentityDbContext<User>
    {
        public static Database Create()
        {
            return new Database();
        }


        public DbSet<Food> Foods { get; set; }
        public DbSet<EnteredFood> EnteredFoods { get; set; }
        public DbSet<UsersToFood> UsersToFoods { get; set; }
        public DbSet<FoodsUnit> FoodsUnits { get; set; }
    }
}
