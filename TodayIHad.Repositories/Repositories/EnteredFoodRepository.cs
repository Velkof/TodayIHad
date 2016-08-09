using System;
using System.Collections.Generic;
using System.Linq;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class EnteredFoodRepository : IEnteredFoodRepository
    {
        private Database db = new Database();

        public bool Create(EnteredFood enteredFood)
        {
            enteredFood.DateCreated = DateTime.Now;
            enteredFood.DateUpdated = DateTime.Now;

            db.EnteredFoods.Add(enteredFood);
            db.SaveChanges();

            return true;
            
        }

        public bool Delete(int id)
        {
            var dbEnteredFood = GetById(id);

            if (dbEnteredFood != null)
            {
                db.EnteredFoods.Remove(dbEnteredFood);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<EnteredFood> GetAll()
        {
            return db.EnteredFoods.ToList();
        }

        //public List<EnteredFood> GetAllForCurrentUser(string userid)
        //{
        //    throw new NotImplementedException();
        //}

        public EnteredFood GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(EnteredFood enteredFood)
        {
            var dbEnteredFood = GetById(enteredFood.Id);

            if (dbEnteredFood != null)
            {
                dbEnteredFood.Amount = enteredFood.Amount;
                dbEnteredFood.Unit = enteredFood.Unit;
                dbEnteredFood.DateUpdated = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
