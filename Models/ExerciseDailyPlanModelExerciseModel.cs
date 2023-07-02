using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fitt.Data;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace fitt.Models
{
    public class ExerciseDailyPlanModelExerciseModel
    {
        [Key]
        public int ExerciseDailyPlanExerciseId { get; set; }


        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }

        public virtual ExerciseModel Exercise { get; set; }


        [ForeignKey("ExerciseDailyPlan")]
        public int ExerciseDailyPlanId { get; set; }
        public virtual ExerciseDailyPlanModel ExerciseDailyPlan { get; set; }
    }


}
