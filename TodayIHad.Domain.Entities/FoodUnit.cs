using System.ComponentModel.DataAnnotations;

namespace TodayIHad.Domain.Entities
{
    [MetadataType(typeof(FoodUnitMetadata))]
    public class FoodUnit
    {
        public int Id { get; set; }

        [MaxLength(84)]
        public string Name { get; set; }
        public double GramWeight { get; set; }

        public virtual int FoodId { get; set; }
        //public virtual Food Food { get; set; }

    }

    public class FoodUnitMetadata
    {
        [Required(ErrorMessage = "Unit name is required")]
        [StringLength(84, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 84 characters")]
        [RegularExpression("^[a-zA-Z0-9-_() ,.%\\/]*$", ErrorMessage = "Name can contain only letters, numbers, and the symbols - , . _ / % ()")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Weight in grams is a required field")]
        [Range(0, 9999, ErrorMessage = "Value must be between {1} and {2}")]
        public double? GramWeight { get; set; }
    }

}
