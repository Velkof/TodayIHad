using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IEnteredFoodRepository
    {
        List<EnteredFood> GetAll();
        EnteredFood GetById(int id);
        List<EnteredFood> GetAllForCurrentUser();

        bool Create(EnteredFood enteredFood);
        bool Update(EnteredFood enteredFood);
        bool Delete(int id);
    }
}
