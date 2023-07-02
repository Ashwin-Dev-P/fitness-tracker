using System.ComponentModel.DataAnnotations;

namespace fitt.Models
{
    public class ExercisePlanModel
    {
        [Key]
        public int ExercisePlanId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }


    }
}
