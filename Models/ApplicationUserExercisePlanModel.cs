using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitt.Models
{
    public class ApplicationUserExercisePlanModel
    {
        [Key]
        public int ApplicationUserExercisePlanId { get; set; }

        [ForeignKey("ExercisePlan")]
        public int ExercisePlanId { get; set; }
        public virtual ExercisePlanModel ExercisePlan { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}
