using fitt.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace fitt.Dao.BodyWeightsDao
{
    public class BodyWeightsModelPostDao
    {
        

        public double BodyWeight { get; set; }

        public DateTime? RecordedDate { get; set; }


       
        
    }
}
