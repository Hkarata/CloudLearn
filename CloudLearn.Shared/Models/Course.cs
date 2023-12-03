using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudLearn.Shared.Models
{
    [PrimaryKey("CourseCode")]
    public class Course
    {
        [Required]
        [MaxLength(5, ErrorMessage = "This is very long course code")]
        public required string CourseCode { get; set; }

        [Required]
        public required string CourseName { get; set; }

        [Required]
        public required string CourseDescription { get; set; }

        [Required]
        public required string EnrollmentKey { get; set; }
        public required Lecturer Lecturer { get; set; }
        public List<Student>? EnrolledStudents { get; set; }
        public List<AcademicYear>? AcademicYears { get; set; }
        public List<DegreeProgram>? DegreeProgram { get; set;}
        public bool IsDeleted { get; set; }

    }
}
