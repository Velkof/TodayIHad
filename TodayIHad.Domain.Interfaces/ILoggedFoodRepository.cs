using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface ILoggedFoodRepository
    {
        List<LoggedFood> GetAll();
        LoggedFood GetById(int id);
        List<LoggedFood> GetAllForCurrentUser();

        bool Create(LoggedFood loggedFood);
        bool Update(LoggedFood loggedFood);
        bool Delete(int id);
    }
}
