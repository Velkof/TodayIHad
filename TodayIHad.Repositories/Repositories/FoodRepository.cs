using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private Database db = new Database();


        public bool Create(Food food)
        {
            food.IsDefault = 1;

            db.Foods.Add(food);
            db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var dbFood = GetById(id);

            if (dbFood != null)
            {
                db.Foods.Remove(dbFood);
                db.SaveChanges();

                return true;
            }
            return false;
        }

        public List<Food> GetAll()
        {
            return db.Foods.ToList();
        }

        public List<Food> GetAllCreatedByCurrentUser()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            List<UsersToFood> usersToFoodsForCurrentUser = db.UsersToFoods.Where(x => x.UserId == userId).ToList();

            List<Food> foodsCreatedByUser = new List<Food>();

            foreach (var u in usersToFoodsForCurrentUser)
            {
                Food food = db.Foods.FirstOrDefault(x => x.Id == u.FoodId);
                foodsCreatedByUser.Add(food);
            }

            return foodsCreatedByUser;
        }


        public Food GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);

        }

        public bool Update(Food food)
        {
            var dbFood = GetById(food.Id);

            if (dbFood != null)
            {


                dbFood.Name = food.Name;
                dbFood.Calories = food.Calories;
                dbFood.ProteinGr = food.ProteinGr;
                dbFood.FatGr = food.FatGr;
                dbFood.CarbsGr = food.CarbsGr;
                dbFood.FiberGr = food.FiberGr;
                dbFood.SugarGr = food.SugarGr;
                dbFood.SodiumMg = food.SodiumMg;
                dbFood.FatSatGr = food.FatSatGr;
                dbFood.FatMonoGr = food.FatMonoGr;
                dbFood.FatPolyGr = food.FatPolyGr;
                dbFood.CholesterolMg = food.CholesterolMg;
                                
                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
