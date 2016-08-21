using System;
using System.ComponentModel.DataAnnotations;

namespace TodayIHad.Domain.Entities
{
    public class LoggedFood
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public int Amount { get; set; }

        [MaxLength(84)]
        public string Unit { get; set; }

        public int Calories { get; set; }
        public float? ProteinGr { get; set; }
        public float? FatGr { get; set; }
        public float? CarbsGr { get; set; }
        public float? FiberGr { get; set; }
        public float? SugarGr { get; set; }
        public int? SodiumMg { get; set; }
        public float? FatSatGr { get; set; }
        public float? FatMonoGr { get; set; }
        public float? FatPolyGr { get; set; }
        public int? CholesterolMg { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }



        public virtual int FoodId { get; set; }
        //public virtual Food Food { get; set; }

        [MaxLength(128)]
        public virtual string UserId { get; set; }
        //public virtual User User { get; set; }
          

    }
}
