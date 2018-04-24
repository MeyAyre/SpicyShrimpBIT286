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
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Prefix { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public int classroomCode { get; set; }
        public virtual Student 
    }
}
