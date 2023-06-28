using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitt.Models
{
    public class ExerciseModel
    {


        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string?  Description { get; set; }

        public string? ImageExtension { get; set; }

    }
}
