using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodayIHad.Domain.Entities;
using TodayIHad.Domain.Interfaces;

namespace TodayIHad.Repositories
{
    public class UserScoreRepository : IUserScoreRepository
    {
        private Database db = new Database();

        public bool Create(UserScore userScore)
        {
            userScore.DateCreated = DateTime.Now;
            userScore.DateUpdated = DateTime.Now;
            userScore.ActiveDays = 1;
            userScore.Streak = 1;
            userScore.Level = 0;
            userScore.Score = 15;
            userScore.Rank = 1;

            db.UserScores.Add(userScore);
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var dbUserScore = GetById(id);

            if (dbUserScore != null)
            {
                db.UserScores.Remove(dbUserScore);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<UserScore> GetAll()
        {
            return db.UserScores.ToList();
        }

        public UserScore GetForCurrentUser()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            return GetAll().FirstOrDefault(x => x.UserId == userId);
        }

        public UserScore GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(UserScore userScore)
        {

            userScore.DateUpdated = DateTime.Now;
            userScore.Streak = userScore.Streak + 1;
            userScore.ActiveDays = userScore.ActiveDays + 1;
            userScore.Score = userScore.Score + userScore.Streak * 10 + userScore.ActiveDays * 5;
            userScore.Level = CalculateUserLevel(userScore.Score);
            userScore.Rank = CalculateUserRank(userScore.Score);

            db.SaveChanges();

            return true;
        }

        public int CalculateUserLevel(int score)
        {
            if (score < 25)
                return 0;
            else if (score < 50)
                return 1;
            else if (score < 100)            
                return 2;            
            else if (score < 250)            
                return 3;            
            else if (score < 500)            
                return 4;
            else if (score < 1000)            
                return 5;
            else if (score < 2500)
                return 6;
            else if (score < 5000)
                return 7;
            else if (score < 10000)            
                return 8;
            else if (score < 25000)
                return 9;
            else if (score < 50000)
                return 10;
            else if (score < 100000)
                return 11;
            else if (score < 250000)
                return 12;
            else if (score < 500000)
                return 13;
            else if (score < 1000000)
                return 14;
            else
                return 15; 
        }


        public int CalculateUserRank(int score)
        {
            return 1;
        }

    }
}
