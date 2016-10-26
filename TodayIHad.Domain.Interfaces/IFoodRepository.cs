using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IFoodRepository
    {
        List<Food> GetAll();
        Food GetById(int id);
        List<Food> GetDefaultFoods();
        List<Food> GetAllCreatedByCurrentUser();

        bool Create(Food food);
        bool Update(Food food);
        bool Delete(int id);
    }
}
