using System.ComponentModel.DataAnnotations;

namespace fitt.Dao
{
    public class ExerciseTypeModelDao
    {

        public int ExerciseTypeId { get; set; }

     
        public required string Name { get; set; }

        
        public string? Description { get; set; }

        
    }
}
