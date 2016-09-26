using System;

namespace TodayIHad.Domain.Entities
{
    public class UserScore
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public int NextLevel { get; set; }
        public int ScoreToNextLevel { get; set;}
        public int PercentOfLevel { get; set; }
        public int Streak { get; set; }
        public int ActiveDays { get; set; }
        public int Rank { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual string UserId { get; set; }
    }
}
