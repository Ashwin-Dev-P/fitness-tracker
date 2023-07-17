using System.ComponentModel.DataAnnotations;

namespace fitt.Dao
{
    public class ExerciseModelGetDao
    {
        public int ExerciseId { get; set; }

       
        public required string Name { get; set; }

        

        public string? ImageExtension { get; set; }
    }
}
