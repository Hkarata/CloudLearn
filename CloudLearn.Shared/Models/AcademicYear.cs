using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudLearn.Shared.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public required string Year { get; set; }
        public List<Course>? YearCourses { get; set; }
        public bool IsDeleted { get; set; }
    }
}
