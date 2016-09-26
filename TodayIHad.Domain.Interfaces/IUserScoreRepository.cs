using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IUserScoreRepository
    {
        List<UserScore> GetAll();
        UserScore GetById(int id);
        UserScore GetForCurrentUser(string userId);

        bool Create(UserScore userScore);
        bool Update(UserScore userScore);
        bool Delete(int id);
    }
}
