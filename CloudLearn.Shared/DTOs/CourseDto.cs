using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudLearn.Shared.DTOs
{
    public record struct CourseDto(String CourseCode, String CourseName, String CourseDescription, String? LecturerName, String? UserId, List<ProgramDto>? Programs, List<YearDto>? Years);
}
