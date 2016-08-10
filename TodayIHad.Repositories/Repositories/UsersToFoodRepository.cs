using System;
using System.Collections.Generic;
using System.Linq;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class UsersToFoodRepository : IUsersToFoodRepository
    {
        private Database db = new Database();

        public bool Create(UsersToFood usersToFood)
        {
            usersToFood.DateCreated = DateTime.Now;
            usersToFood.DateUpdated = DateTime.Now;

            db.UsersToFoods.Add(usersToFood);

            return true;
        }

        public bool Delete(int id)
        {
            var dbUsersToFood = GetById(id);

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
