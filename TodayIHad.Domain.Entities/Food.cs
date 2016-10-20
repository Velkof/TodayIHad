using System.ComponentModel.DataAnnotations;

namespace TodayIHad.Domain.Entities
{
    [MetadataType(typeof(FoodMetadata))]
    public class Food
    {
        public int Id { get; set; }

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

    public class FoodMetadata
    {
        [Required(ErrorMessage = "Food name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 200 characters")]
        [RegularExpression("^[a-zA-Z0-9-_(),.%/]*$", ErrorMessage = "Name can contain only letters, numbers, and the symbols - , . _ / % ()")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Calories is a required field")]
        [Range(0, 99999, ErrorMessage = "Calories must be between {1} and {2}")]
        public double? CaloriesKcal { get; set; }

        [Required(ErrorMessage = "Protein is a required field")]
        [Range(0, 99999, ErrorMessage ="Protein must be between {1} and {2}")]
        public double? ProteinGr { get; set; }


        [Required(ErrorMessage = "Fat is a required field")]
        [Range(0, 99999, ErrorMessage = "Fat must be between {1} and {2}")]
        public double? FatGr { get; set; }

        [Required(ErrorMessage = "Carbs is a required field")]
        [Range(0, 99999, ErrorMessage = "Carbs must be between {1} and {2}")]
        public double? CarbsGr { get; set; }

        [Range(0, 99999, ErrorMessage = "Fiber must be between {1} and {2}")]
        public double? FiberGr { get; set; }

        [Range(0, 99999, ErrorMessage = "Sugar must be between {1} and {2}")]
        public double? SugarGr { get; set; }

        [Range(0, 99999, ErrorMessage = "Sodium must be between {1} and {2}")]
        public double? SodiumMg { get; set; }

        [Range(0, 99999, ErrorMessage = "Sat. Fat must be between {1} and {2}")]
        public double? FatSatGr { get; set; }

        [Range(0, 99999, ErrorMessage = "Mono. Fat must be between {1} and {2}")]
        public double? FatMonoGr { get; set; }

        [Range(0, 99999, ErrorMessage = "Poly. Fat must be between {1} and {2}")]
        public double? FatPolyGr { get; set; }

        [Range(0, 99999, ErrorMessage = "Cholesterol must be between {1} and {2}")]
        public double? CholesterolMg { get; set; }

        public int IsDefault { get; set; }
    }
}