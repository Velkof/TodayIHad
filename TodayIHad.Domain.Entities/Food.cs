using System.Collections.Generic;

namespace TodayIHad.Domain.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaloriesKcal { get; set; }
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
        public int IsDefault { get; set; }
        
        public virtual ICollection<FoodUnit> FoodsUnits { get; set; }
    }
}
