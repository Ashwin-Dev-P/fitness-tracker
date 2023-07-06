using fitt.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitt.Dao
{
    public class IntensityModelDao
    {
        public double weights { get; set; }

        public int? repetitions { get; set; }

        public DateTime? ExerciseDate { get; set; }

        public int ExerciseId { get; set; }
        
    }
}
