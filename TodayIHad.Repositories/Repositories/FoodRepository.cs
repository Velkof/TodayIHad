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
                dbFood.Calories_kcal = food.Calories_kcal;
                dbFood.Protein_gr = food.Protein_gr;
                dbFood.Fat_gr = food.Fat_gr;
                dbFood.Carbs_gr = food.Carbs_gr;
                dbFood.Fiber_gr = food.Fiber_gr;
                dbFood.Sugar_gr = food.Sugar_gr;
                dbFood.Sodium_mg = food.Sodium_mg;
                dbFood.Fat_Sat_gr = food.Fat_Sat_gr;
                dbFood.Fat_Mono_gr = food.Fat_Mono_gr;
                dbFood.Fat_Poly_gr = food.Fat_Poly_gr;
                dbFood.Cholesterol_mg = food.Cholesterol_mg;
                
                
                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
