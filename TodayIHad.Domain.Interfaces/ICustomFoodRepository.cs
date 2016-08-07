using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface ICustomFoodRepository
    {
        List<CustomFood> GetAll();
        CustomFood GetById(int id);
        bool Create(CustomFood customFood);
        bool Update(CustomFood customFood);
        bool Delete(int id);
    }
}
