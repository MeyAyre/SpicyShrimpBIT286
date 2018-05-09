using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace BIT285_SpicyShrimps.Models
{
    public class Activity
    {
        //defines the entities needed for the Activity model
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityPassword { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ActivityDate { get; set; }
    }
}
