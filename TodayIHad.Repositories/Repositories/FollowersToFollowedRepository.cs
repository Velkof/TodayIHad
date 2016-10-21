using Microsoft.AspNet.Identity;
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

        public bool Delete(string followedUserId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            var followedUser = db.FollowersToFolloweds.Where(x=>x.FollowerId == userId).FirstOrDefault(x => x.FollowedId == followedUserId);
                
            db.FollowersToFolloweds.Remove(followedUser);

            db.SaveChanges();
            return true;

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

        public List<FollowersToFollowed> GetAllThatFollowUser()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            return GetAll().Where(x => x.FollowedId == userId).ToList();
        }
    }
}
