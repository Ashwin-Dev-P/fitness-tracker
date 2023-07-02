using System.ComponentModel.DataAnnotations;

namespace fitt.Dao
{
    public class ExerciseDailyPlanModelDao
    {
        public int ExerciseDailyPlanId { get; set; }

        public string Name { get; set; }

        
        public string? Description { get; set; }
    }
}
