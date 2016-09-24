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
        public int Rank { get; set; }
        public DateTime ScoreLastUpdated { get; set; }
    }
}
