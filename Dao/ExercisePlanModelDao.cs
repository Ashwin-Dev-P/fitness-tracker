using System.ComponentModel.DataAnnotations;

namespace fitt.Dao
{
    public class ExercisePlanModelDao
    {
        public int ExercisePlanId { get; set; }

        public string Name { get; set; }

        
        public string? Description { get; set; }

        public int ExerciseTypeId { get; set; }
    }
}
