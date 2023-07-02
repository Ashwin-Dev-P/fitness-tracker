using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitt.Models
{
    public class ExercisePlanExerciseDailyPlanModel
    {
        [Key]
        public int ExercisePlanExerciseDailyPlanId { get; set; }

        [ForeignKey("ExercisePlan")]
        public int ExercisePlanId { get; set; }
        public virtual ExercisePlanModel ExercisePlan { get; set; }

        [ForeignKey("ExerciseDailyPlan")]
        public int ExerciseDailyPlanId { get; set; }
        public virtual ExerciseDailyPlanModel ExerciseDailyPlan { get; set; }
    }
}
