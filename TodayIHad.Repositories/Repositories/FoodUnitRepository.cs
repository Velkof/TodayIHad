using System.Collections.Generic;
using System.Linq;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class FoodUnitRepository : IFoodUnitRepository
    {
        private Database db = new Database();

        public bool Create(List<FoodUnit> foodUnitsList, int foodId)
        {

            foreach(var foodUnit in foodUnitsList)
            {
                foodUnit.FoodId = foodId;
                db.FoodUnits.Add(foodUnit);
            }


            db.SaveChanges();

            return true;

        }

        public bool Delete(int foodId)
        {
            var dbFoodUnitList = GetAllForFood(foodId);



            if (dbFoodUnitList != null)
            {
                foreach (var i in dbFoodUnitList)
                {
                    db.FoodUnits.Remove(i);
                }
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public List<FoodUnit> GetAll()
        {
            return db.FoodUnits.ToList();
        }

        public List<FoodUnit> GetAllForFood(int foodId)
        {
            return db.FoodUnits.Where(x => x.FoodId == foodId).ToList();
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
