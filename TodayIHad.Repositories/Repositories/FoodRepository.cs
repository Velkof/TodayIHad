using System;
using System.Collections.Generic;
using System.Linq;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private Database db = new Database();
        private IUsersToFoodRepository _usersToFoodRepository = new UsersToFoodRepository();


        public bool Create(Food food)
        {
            food.IsDefault = 1;

            //var userId = HttpContext.Current.User.Identity.GetUserId();


            //var dbUsersToFood = new UsersToFood
            //{
            //    FoodId = food.Id,
            //    DateCreated = DateTime.Now,
            //    DateUpdated = DateTime.Now,
            //    UserId = userId,
            //};

            //db.UsersToFoods.Add(dbUsersToFood);

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
            var dbUsersToFood = db.UsersToFoods.FirstOrDefault(x => x.FoodId == dbFood.Id);

            if (dbFood != null && dbUsersToFood != null)
            {

                dbUsersToFood.DateUpdated = DateTime.Now;

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
