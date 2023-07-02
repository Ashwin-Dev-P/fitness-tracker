using System.ComponentModel.DataAnnotations;

namespace fitt.Models
{
    public class ExerciseTypeModel
    {
        [Key]
        public int ExerciseTypeId { get; set; }

        [Required]
        public required string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        public string? ImageExtension { get; set; }
    }
}
