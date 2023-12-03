using CloudLearn.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudLearn.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<DegreeProgram> DegreePrograms { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicYear>().HasData(
                new AcademicYear { Id = 1, Year = "First Year"},
                new AcademicYear { Id = 2, Year = "Second Year" },
                new AcademicYear { Id = 3, Year = "Third Year" },
                new AcademicYear { Id = 4, Year = "Fourth Year" }
                );

            modelBuilder.Entity<DegreeProgram>().HasData(
                new DegreeProgram { Id = 1, Program = "B. Sc. Computer Science"},
                new DegreeProgram { Id = 2, Program = "B. Sc. Telecommunications" },
                new DegreeProgram { Id = 3, Program = "B. Sc. Computer Eng and IT" },
                new DegreeProgram { Id = 4, Program = "B. Sc. Business and IT" },
                new DegreeProgram { Id = 5, Program = "B. Sc. Electronic Science" }
                );

            modelBuilder.Entity<AcademicYear>().HasIndex(x => x.Year).IsUnique(true);
            modelBuilder.Entity<DegreeProgram>().HasIndex(x => x.Program).IsUnique(true);
            modelBuilder.Entity<Course>().HasIndex(x => x.CourseName).IsUnique(true);
            modelBuilder.Entity<Lecturer>().HasIndex(x => x.LecturerName).IsUnique(true);
            modelBuilder.Entity<Student>().HasIndex(x => x.Name).IsUnique(true);

        }

    }
}
