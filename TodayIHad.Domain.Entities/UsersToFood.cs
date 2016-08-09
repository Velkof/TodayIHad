using System;

namespace TodayIHad.Domain.Entities
{
    public class UsersToFood
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual string UserId { get; set; }
        public virtual int FoodId { get; set; }
    }
}
