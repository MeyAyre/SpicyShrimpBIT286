using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BIT285_SpicyShrimps.Models
{
    public class Teacher
    {
        [Key]
        [Required]
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Prefix { get; set; }
        public string Username { get { return Prefix + ". " + LastName; } }
        [Required]
        public string Password { get; set; }
        public string ClassroomCode { get; set; }
        public string Level { get; set; }
        /*Collection of Students*/
        public virtual ICollection<Student> Students { get; set; }
    }
}
