using System.Collections.Generic;

namespace TodayIHad.Domain.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories_kcal { get; set; }
        public float Protein_gr { get; set; }
        public float Fat_gr { get; set; }
        public float Carbs_gr { get; set; }
        public float Fiber_gr { get; set; }
        public float Sugar_gr { get; set; }
        public int Sodium_mg { get; set; }
        public float Fat_Sat_gr { get; set; }
        public float Fat_Mono_gr { get; set; }
        public float Fat_Poly_gr { get; set; }
        public int Cholesterol_mg { get; set; }
        public int IsDefault { get; set; }
        
        public virtual ICollection<FoodsUnit> FoodsUnits { get; set; }
    }
}
