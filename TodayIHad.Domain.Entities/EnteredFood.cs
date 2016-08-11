using System;

namespace TodayIHad.Domain.Entities
{
    public class EnteredFood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoodId { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual string UserId { get; set; }
        public virtual User User { get; set; }
          

    }
}
