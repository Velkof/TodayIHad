using System;
using System.ComponentModel.DataAnnotations;

namespace TodayIHad.Domain.Entities
{

    [MetadataType(typeof(LoggedFoodMetadata))]
    public class LoggedFood
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }

        public double? Calories { get; set; }
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

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }



        public virtual int FoodId { get; set; }
        //public virtual Food Food { get; set; }

        [MaxLength(128)]
        public virtual string UserId { get; set; }
        //public virtual User User { get; set; }
          

    }

    public class LoggedFoodMetadata
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z0-9-_(),.%\\/]*$")]
        public string Name { get; set; }

        [Required]
        [Range(0, 999, ErrorMessage = "Calories must be between {1} and {2}")]
        public int Amount { get; set; }


        [Required]
        [StringLength(84, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z0-9-_(),.%\\/]*$")]
        public string Unit { get; set; }

        [Required]
        [Range(0, 99999)]
        public double? Calories { get; set; }

        [Required]
        [Range(0, 99999)]
        public double? ProteinGr { get; set; }


        [Required]
        [Range(0, 99999)]
        public double? FatGr { get; set; }

        [Required]
        [Range(0, 99999)]
        public double? CarbsGr { get; set; }

        [Range(0, 99999)]
        public double? FiberGr { get; set; }

        [Range(0, 99999)]
        public double? SugarGr { get; set; }

        [Range(0, 99999)]
        public double? SodiumMg { get; set; }

        [Range(0, 99999)]
        public double? FatSatGr { get; set; }

        [Range(0, 99999)]
        public double? FatMonoGr { get; set; }

        [Range(0, 99999)]
        public double? FatPolyGr { get; set; }

        [Range(0, 99999)]
        public double? CholesterolMg { get; set; }

    }
}
