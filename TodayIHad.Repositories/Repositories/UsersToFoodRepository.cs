using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class UsersToFoodRepository : IUsersToFoodRepository
    {
        private Database db = new Database();

        public bool Create(int foodId)
        {
            var usersToFood = new UsersToFood
            {
                FoodId = foodId,
                UserId = HttpContext.Current.User.Identity.GetUserId(),
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            db.UsersToFoods.Add(usersToFood);
            db.SaveChanges();

            return true;
        }

        public bool Delete(int foodId)
        {
            var dbUsersToFood = GetByFoodId(foodId);

            if (dbUsersToFood != null)
            {
                db.UsersToFoods.Remove(dbUsersToFood);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public List<UsersToFood> GetAll()
        {
            return db.UsersToFoods.ToList();
        }

        public UsersToFood GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public UsersToFood GetByFoodId(int foodId)
        {
            return GetAll().FirstOrDefault(x => x.FoodId == foodId);
        }

        public bool Update(UsersToFood usersToFood)
        {
            var dbUsersToFood = GetById(usersToFood.Id);

            if (dbUsersToFood != null)
            {
                dbUsersToFood.DateUpdated = DateTime.Now;

                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
