using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IFoodUnitRepository
    {
        List<FoodUnit> GetAll();
        FoodUnit GetById(int id);
        List<FoodUnit> GetAllForCurrentFood(int foodId);

        bool Create(List<FoodUnit> foodsUnitsList);
        bool Update(List<FoodUnit> foodsUnitsList);
        bool Delete(int id);
    }
}
