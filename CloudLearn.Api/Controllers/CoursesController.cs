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
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Lecturer/{UserId}/Courses")]
        public async Task<ActionResult<List<CourseDto>>> GetTeachingCoursesForLecturer(string UserId)
        {
            var teachingCourses = await _context.Courses
                .AsNoTracking()
                .Include(c => c.AcademicYears)
                .Include(c => c.DegreeProgram)
                .Where(course => course.Lecturer.UserId == UserId)
                .Select(c => new CourseDto
                {
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    LecturerName = c.Lecturer.LecturerName,
                    Programs = c.DegreeProgram.Select(d => new ProgramDto { Program = d.Program}).ToList(),
                    Years = c.AcademicYears.Select(y => new YearDto { Year = y.Year}).ToList(),
                    UserId = UserId
                })
                .ToListAsync();

            return teachingCourses;
        }


        // GET: api/Courses
        [HttpGet]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return await _context.Courses
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .Include(c => c.Lecturer)
                .Include(c => c.AcademicYears)
                .Include(c => c.DegreeProgram)
                .Select(c => new CourseDto
                {
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    LecturerName = c.Lecturer.LecturerName,
                    Programs = c.DegreeProgram.Select(d => new ProgramDto { Program = d.Program}).ToList(),
                    Years = c.AcademicYears.Select(y => new YearDto { Year = y.Year}).ToList(),
                })
                .ToListAsync();
#pragma warning restore CS8604 // Possible null reference argument.
        }

        // GET: api/Courses/5
        [HttpGet("{CourseCode}")]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<ActionResult<CourseDto>> GetCourse(string CourseCode)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            CourseDto? course = await _context.Courses
                .AsNoTracking()
                .Include(c => c.Lecturer)
                .Select(c => new CourseDto
                {
                    CourseCode = c.CourseCode,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    LecturerName = c.Lecturer.LecturerName,
                    Programs = c.DegreeProgram.Select(d => new ProgramDto { Program = d.Program}).ToList(),
                    Years = c.AcademicYears.Select(y => new YearDto { Year = y.Year}).ToList(),
                })
                .FirstOrDefaultAsync(c => c.CourseCode == CourseCode);
#pragma warning restore CS8604 // Possible null reference argument.

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<IActionResult> PutCourse(string id, CourseDto course)
        {
            if (id != course.CourseCode)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<ActionResult<Course>> PostCourse(CourseDto course)
        {

            var programNames = course.Programs?.Select(p => p.Program).ToList();
            if (programNames is not null)
            {
                var programs = await _context.DegreePrograms
                                .Where(d => programNames.Contains(d.Program))
                                .ToListAsync();
            }

            var yearNames = course.Years?.Select(y => y.Year).ToList();
            if (yearNames is not null)
            {
                var years = await _context.AcademicYears
                    .Where(y => yearNames.Contains(y.Year))
                    .ToListAsync();
            }

            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(l => l.LecturerName == course.LecturerName);

            if (lecturer is not null)
            {
                var Course = new Course
                {
                    CourseCode = course.CourseCode,
                    CourseName = course.CourseName,
                    CourseDescription = course.CourseDescription,
                    EnrollmentKey = course.CourseCode,
                    Lecturer = lecturer
                };

                _context.Courses.Add(Course);
            }
                
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseExists(course.CourseCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourse", new { id = course.CourseCode }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(string CourseCode)
        {
            return _context.Courses.Any(e => e.CourseCode == CourseCode);
        }
    }
}
