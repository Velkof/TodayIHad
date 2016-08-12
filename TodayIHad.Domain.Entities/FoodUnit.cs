using System.ComponentModel.DataAnnotations;

namespace TodayIHad.Domain.Entities
{
    public class FoodUnit
    {
        public int Id { get; set; }

        [MaxLength(84)]
        public string Name { get; set; }
        public float GramWeight { get; set; }

        public virtual int FoodId { get; set; }
        public virtual Food Food { get; set; }

    }
}
