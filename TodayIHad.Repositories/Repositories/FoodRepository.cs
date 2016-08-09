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

            throw new NotImplementedException();
        }

        public bool Update(Food food)
        {
            throw new NotImplementedException();
        }
    }
}
