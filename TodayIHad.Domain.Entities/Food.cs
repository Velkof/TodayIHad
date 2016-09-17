using System.ComponentModel.DataAnnotations;

namespace TodayIHad.Domain.Entities
{
    public class Food
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public double? CaloriesKcal { get; set; }
        public double? ProteinGr { get; set; }
        public double? FatGr { get; set; }
        public double? CarbsGr { get; set; }
        public double? FiberGr { get; set; }
        public double? SugarGr { get; set; }
        public double? SodiumMg { get; set; }
        public double? FatSatGr { get; set; }
        public double? FatMonoGr { get; set; }
        public double? FatPolyGr { get; set; }
        public double? CholesterolMg { get; set; }

        public int IsDefault { get; set; }

        //public virtual ICollection<FoodUnit> FoodUnits { get; set; }
    }
}
