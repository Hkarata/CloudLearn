using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudLearn.Shared.Models
{
    [PrimaryKey("UserId")]
    public class Lecturer
    {
        public required string UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "This name is very long")]
        public required string LecturerName { get; set; }
        public List<Course>? TeachingCourses { get; set; }
        public bool IsDeleted { get; set; }

    }
}
