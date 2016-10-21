using System;

namespace TodayIHad.Domain.Entities
{
    public class UserScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public int Streak { get; set; }
        public int ActiveDays { get; set; }
        public int SevenDayScore { get; set; }
        public DateTime SevenDayScoreLastReset { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
    }
}
