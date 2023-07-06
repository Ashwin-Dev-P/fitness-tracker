using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitt.Models
{
    public class IntensityModel
    {
        [Key]
        public int IntensityId { get; set; }

         public double weights { get; set; }

        public int? repetitions { get; set; }

        public DateTime? ExerciseDate { get; set; }



        [ForeignKey("Exercise")]  public int ExerciseId { get; set; }
        public virtual ExerciseModel Exercise { get; set; }





        [ForeignKey("ApplicationUser")]  public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }



    }
}
