using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories.Repositories
{
    public class FollowersToFollowedRepository : IFollowersToFollowedRepository
    {

        private Database db = new Database();


        public bool Create(string followedUserId)
        {
            FollowersToFollowed newFollowersToFollowed = new FollowersToFollowed();

            newFollowersToFollowed.FollowerId = HttpContext.Current.User.Identity.GetUserId();
            newFollowersToFollowed.FollowedId = followedUserId;

            db.FollowersToFolloweds.Add(newFollowersToFollowed);
            db.SaveChanges();

            return true;
        }

        public bool Delete(string followerId)
        {
            throw new NotImplementedException();
        }

        public List<FollowersToFollowed> GetAll()
        {
            return db.FollowersToFolloweds.ToList();
        }

        public List<FollowersToFollowed> GetAllFollowedByUser()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            return GetAll().Where(x => x.FollowerId == userId).ToList();
        }
    }
}
