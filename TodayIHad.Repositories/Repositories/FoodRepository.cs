using System.Collections.Generic;
using System.Linq;
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
                dbFood.CaloriesKcal = food.CaloriesKcal;
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
