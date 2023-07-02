using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitt.Models
{
    public class ExercisePlanModel
    {
        [Key]
        public int ExercisePlanId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }


        [ForeignKey("ExerciseType")]
        public int ExerciseTypeId { get; set; }
        public virtual ExerciseTypeModel ExerciseType { get; set; }


        public virtual ICollection<ExerciseDailyPlanModel> ExerciseDailyPlans { get; set; }


    }
}
