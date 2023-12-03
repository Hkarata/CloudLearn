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
    public class Student
    {
        public required string UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "This name is very long")]
        public required string Name { get; set; }

        [Required]
        public required DegreeProgram Program { get; set; }

        [Required]
        public required AcademicYear Year { get; set; }
        public List<Course>? EnrolledCourses { get; set; }
        public bool IsDeleted { get; set; }
    }
}
