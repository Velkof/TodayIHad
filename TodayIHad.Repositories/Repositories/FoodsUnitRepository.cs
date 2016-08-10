using System.Collections.Generic;
using System.Linq;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class FoodsUnitRepository : IFoodsUnitRepository
    {
        private Database db = new Database();

        public bool Create(List<FoodsUnit> foodsUnitsList)
        {
            
            foreach (var foodsUnit in foodsUnitsList)
            {
                db.FoodsUnits.Add(foodsUnit);
            }

            db.SaveChanges();

            return true;

        }

        public bool Delete(int id)
        {
            var dbFoodsUnit = GetById(id);

            if (dbFoodsUnit != null)
            {
                db.FoodsUnits.Remove(dbFoodsUnit);
                db.SaveChanges();

                return true;
            }

            return false;
        }

        public List<FoodsUnit> GetAll()
        {
            return db.FoodsUnits.ToList();
        }

        public List<FoodsUnit> GetAllForCurrentFood(int foodId)
        {
            return GetAll().Where(x => x.FoodId == foodId).ToList();
        }

        public FoodsUnit GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(List<FoodsUnit> foodsUnitsList)
        {
            

            foreach (var foodsUnit in foodsUnitsList)
            {
                var dbFoodsUnit = GetById(foodsUnit.Id);

                if (dbFoodsUnit != null)
                {
                    dbFoodsUnit.Name = foodsUnit.Name;
                    dbFoodsUnit.Amount = foodsUnit.Amount;
                    dbFoodsUnit.GramWeight = foodsUnit.GramWeight;
                    

                    db.SaveChanges();
                    return true;
                }

            }
            return false;

        }
    }
}
