using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace fitt.Models
{
    public class CalorieModel
    {
        [Key] public int CalorieId { get; set; }

        public int CalorieCount { get; set; }

        public DateTime? CalorieConsumptionDate { get; set; }


        public DateTime? CreatedAt { get; set; }

        [ForeignKey("ApplicationUser")] public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
