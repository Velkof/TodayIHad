using System.Collections.Generic;
using System.Linq;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class FoodUnitRepository : IFoodUnitRepository
    {
        private Database db = new Database();

        public bool Create(List<FoodUnit> foodUnitsList)
        {
            
            foreach (var foodUnit in foodUnitsList)
            {
                db.FoodUnits.Add(foodUnit);
            }

            db.SaveChanges();

            return true;

        }

        public bool Delete(int id)
        {
            var dbFoodUnit = GetById(id);

            if (dbFoodUnit != null)
            {
                db.FoodUnits.Remove(dbFoodUnit);
                db.SaveChanges();

                return true;
            }

            return false;
        }

        public List<FoodUnit> GetAll()
        {
            return db.FoodUnits.ToList();
        }

        public List<FoodUnit> GetAllForCurrentFood(int foodId)
        {
            return GetAll().Where(x => x.FoodId == foodId).ToList();
        }

        public FoodUnit GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(List<FoodUnit> foodUnitsList)
        {
            

            foreach (var foodUnit in foodUnitsList)
            {
                var dbFoodUnit = GetById(foodUnit.Id);

                if (dbFoodUnit != null)
                {
                    dbFoodUnit.Name = foodUnit.Name;
                    dbFoodUnit.GramWeight = foodUnit.GramWeight;
                    

                    db.SaveChanges();
                    return true;
                }

            }
            return false;

        }
    }
}
