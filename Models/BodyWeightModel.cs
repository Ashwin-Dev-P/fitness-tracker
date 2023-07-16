using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace fitt.Models
{
    public class BodyWeightModel
    {
        [Key]
        public int BodyWeightId { get; set; }

        public double BodyWeight { get; set; }

        public DateTime? RecordedDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        [ForeignKey("ApplicationUser")] public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
