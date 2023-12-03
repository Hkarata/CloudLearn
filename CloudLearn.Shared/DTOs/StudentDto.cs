using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudLearn.Shared.DTOs
{
    public record struct StudentDto(String UserId, String Name, String Program, String Year);

}
