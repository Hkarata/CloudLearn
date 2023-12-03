using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudLearn.Shared.Models
{
    public class DegreeProgram
    {
        public int Id { get; set; }
        public required string Program { get; set; }
        public List<Course>? ProgramCourses { get; set; }
        public bool IsDeleted { get; set; }
    }
}
