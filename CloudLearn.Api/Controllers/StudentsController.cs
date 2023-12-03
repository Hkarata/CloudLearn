using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudLearn.Api.Data;
using CloudLearn.Shared.Models;
using CloudLearn.Shared.DTOs;
using CloudLearn.Api.Authentication;

namespace CloudLearn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            return await _context.Students
                .Include(s => s.Program)
                .Include(s => s.Year)
                .Select(s => new StudentDto
                {
                    UserId = s.UserId,
                    Name = s.Name,
                    Program = s.Program.Program,
                    Year = s.Year.Year
                })
                .ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{UserId}")]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<ActionResult<StudentDto>> GetStudent(string UserId)
        {
            var query = await _context.Students
                .AsNoTracking()
                .Include(s => s.Program)
                .Include(s => s.Year)
                .FirstOrDefaultAsync(s => s.UserId == UserId);

            if (query == null)
            {
                return NotFound();
            }

            var student = new StudentDto
            {
                UserId = query.UserId,
                Name = query.Name,
                Program = query.Program.Program,
                Year = query.Year.Year
            };

            return student;
        }

        [HttpGet("StudentsCourse{UserId}")]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<ActionResult<StudentCourseDto>> GetStudentCourse(string UserId)
        {
            if (!StudentExists(UserId))
            {
                return NotFound();
            }
            var student = await _context.Students
                .AsNoTracking()
                .Include(s => s.Program)
                .Include(s => s.Year)
                .Include(s => s.EnrolledCourses)
                .Select(s => new StudentCourseDto
                {
                    UserId = s.UserId,
                    Name = s.Name,
                    Program = s.Program.Program,
                    Year = s.Year.Year,
                    Courses = s.EnrolledCourses.Select(c => new CourseDto
                    {
                        CourseCode = c.CourseCode,
                        CourseName = c.CourseName,
                        CourseDescription = c.CourseDescription
                    }).ToList()
                })
                .FirstOrDefaultAsync(s => s.UserId == UserId);

            return student;
        }

        // PUT: api/Students/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{CourseCode}")]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<IActionResult> PutStudent(string CourseCode, StudentDto student)
        {
            if (CourseCode != student.UserId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(CourseCode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<ActionResult<Student>> PostStudent(StudentDto request)
        {
            var program = await _context.DegreePrograms.FirstOrDefaultAsync(i => i.Program == request.Program);
            var year = await _context.AcademicYears.FirstOrDefaultAsync(i => i.Year == request.Year);


            try
            {
                if (program != null && year != null)
                {
                    var student = new Student
                    {
                        UserId = request.UserId,
                        Name = request.Name,
                        Program = program,
                        Year = year
                    };

                    _context.Students.Add(student);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentExists(request.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudent", new { id = request.UserId }, request);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.UserId == id);
        }
    }
}
