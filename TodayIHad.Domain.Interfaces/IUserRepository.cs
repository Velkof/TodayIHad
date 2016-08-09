using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(string id);
        bool Create(User user);
        bool Update(User user);
        bool Delete(string id);
    }
}
