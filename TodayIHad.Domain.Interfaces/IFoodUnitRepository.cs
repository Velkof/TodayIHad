using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IFoodUnitRepository
    {
        List<FoodUnit> GetAll();
        FoodUnit GetById(int id);
        List<FoodUnit> GetAllForCurrentFood(int foodId);

        bool Create(List<FoodUnit> foodUnitsList);
        bool Update(List<FoodUnit> foodUnitsList);
        bool Delete(int id);
    }
}
