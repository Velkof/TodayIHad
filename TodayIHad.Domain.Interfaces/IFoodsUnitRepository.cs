using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IFoodsUnitRepository
    {
        List<FoodsUnit> GetAll();
        FoodsUnit GetById(int id);
        List<FoodsUnit> GetAllForCurrentFood();

        bool Create(List<FoodsUnit> foodsUnitsList);
        bool Update(List<FoodsUnit> foodsUnitsList);
        bool Delete(int id);
    }
}
