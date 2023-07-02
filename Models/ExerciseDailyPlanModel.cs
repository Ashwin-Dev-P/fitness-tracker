using System.ComponentModel.DataAnnotations;

namespace fitt.Models
{
    public class ExerciseDailyPlanModel
    {
        [Key]
        public int ExerciseDailyPlanId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        public virtual ICollection<ExerciseModel>? Exercises { get; set; }
    }
}
