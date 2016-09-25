using Microsoft.AspNet.Identity;
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
                dbLoggedFood.Calories = loggedFood.Calories;
                dbLoggedFood.Amount = loggedFood.Amount;
                dbLoggedFood.Unit = loggedFood.Unit;
                dbLoggedFood.FatGr = loggedFood.FatGr;
                dbLoggedFood.FatSatGr = loggedFood.FatSatGr;
                dbLoggedFood.FatMonoGr = loggedFood.FatMonoGr;
                dbLoggedFood.FatPolyGr = loggedFood.FatPolyGr;
                dbLoggedFood.ProteinGr = loggedFood.ProteinGr;
                dbLoggedFood.CarbsGr = loggedFood.CarbsGr;
                dbLoggedFood.SugarGr = loggedFood.SugarGr;
                dbLoggedFood.FiberGr = loggedFood.FiberGr;
                dbLoggedFood.CholesterolMg = loggedFood.CholesterolMg;
                dbLoggedFood.SodiumMg = loggedFood.SodiumMg;

                dbLoggedFood.DateUpdated = loggedFood.DateUpdated;

                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
