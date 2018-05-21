using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LostInTheWoods.Models
{
    public abstract class BaseEntity {};
    public class Trail : BaseEntity
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [RegularExpression("([0-9]+)")]
        public string Length { get; set; }

        [Required]
        [RegularExpression("([0-9]+)")]
        public string Elevation { get; set; }

        [Required]
        [RegularExpression(@"^(\-?\d+(\.\d+)?)$", ErrorMessage = "Not the correct format for latitude.")]
        [Range(-90, 90)]
        public string Latitude { get; set; }

        [Required]
        [RegularExpression(@"^(\-?\d+(\.\d+)?)$", ErrorMessage = "Not the correct format for longitude.")]
        [Range(-180, 180)]
        public string Longitude { get; set; }
    }
}