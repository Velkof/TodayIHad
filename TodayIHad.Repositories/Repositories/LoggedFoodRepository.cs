using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class LoggedFoodRepository : ILoggedFoodRepository
    {
        private Database db = new Database();

        public bool Create(LoggedFood loggedFood)
        {
            loggedFood.UserId = HttpContext.Current.User.Identity.GetUserId();
            //loggedFood.DateCreated = DateTime.Now;
            loggedFood.DateUpdated = DateTime.Now;


            db.LoggedFoods.Add(loggedFood);
            db.SaveChanges();

            return true;
            
        }

        public bool Delete(int id)
        {
            var dbLoggedFood = GetById(id);

            if (dbLoggedFood != null)
            {
                db.LoggedFoods.Remove(dbLoggedFood);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<LoggedFood> GetAll()
        {
            return db.LoggedFoods.ToList();
        }

        public List<LoggedFood> GetAllForCurrentUser()
        {
            string  userId = HttpContext.Current.User.Identity.GetUserId();
            return GetAll().Where(x => x.UserId == userId).ToList();
        }

        public LoggedFood GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(LoggedFood loggedFood)
        {
            var dbLoggedFood = GetById(loggedFood.Id);

            if (dbLoggedFood != null)
            {
                dbLoggedFood.Amount = loggedFood.Amount;
                dbLoggedFood.Unit = loggedFood.Unit;
                dbLoggedFood.DateUpdated = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
