using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitt.Models
{
    public class ExerciseModel
    {


        [Key]
        public int ExerciseId { get; set; }

        [Required]
        public required string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string?  Description { get; set; }

        public string? ImageExtension { get; set; }

        public virtual ICollection<ExerciseDailyPlanModel>? ExerciseDailyPlans { get; set; }

    }
}
