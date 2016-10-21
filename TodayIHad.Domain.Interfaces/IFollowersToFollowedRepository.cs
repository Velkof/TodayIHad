using System.Collections.Generic;
using TodayIHad.Domain.Entities;

namespace TodayIHad.Domain.Interfaces
{
    public interface IFollowersToFollowedRepository
    {
        List<FollowersToFollowed> GetAll();

        List<FollowersToFollowed> GetAllFollowedByUser();
        List<FollowersToFollowed> GetAllThatFollowUser();

        bool Create(string followedUserId);
        bool Delete(string followerId);
    }
}
