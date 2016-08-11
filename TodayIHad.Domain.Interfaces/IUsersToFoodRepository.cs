using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IUsersToFoodRepository
    {
        List<UsersToFood> GetAll();
        UsersToFood GetById(int id);

        bool Create(int userId);
        bool Update(UsersToFood usersToFood);
        bool Delete(int id);

    }
}
