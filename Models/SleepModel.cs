using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fitt.Models
{
    public class SleepModel
    {
        [Key] public int SleepId { get; set; }

        public TimeSpan SleepDuration { get; set; }

        public  DateTime? SleepDate { get; set; }


        public DateTime? CreatedAt { get; set; }

        [ForeignKey("ApplicationUser")] public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}
