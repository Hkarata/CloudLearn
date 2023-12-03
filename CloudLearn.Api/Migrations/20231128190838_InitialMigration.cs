using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CloudLearn.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DegreePrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Program = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreePrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LecturerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Students_AcademicYears_YearId",
                        column: x => x.YearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_DegreePrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "DegreePrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollmentKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseCode);
                    table.ForeignKey(
                        name: "FK_Courses_Lecturers_LecturerUserId",
                        column: x => x.LecturerUserId,
                        principalTable: "Lecturers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "AcademicYearCourse",
                columns: table => new
                {
                    AcademicYearsId = table.Column<int>(type: "int", nullable: false),
                    YearCoursesCourseCode = table.Column<string>(type: "nvarchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYearCourse", x => new { x.AcademicYearsId, x.YearCoursesCourseCode });
                    table.ForeignKey(
                        name: "FK_AcademicYearCourse_AcademicYears_AcademicYearsId",
                        column: x => x.AcademicYearsId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicYearCourse_Courses_YearCoursesCourseCode",
                        column: x => x.YearCoursesCourseCode,
                        principalTable: "Courses",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseDegreeProgram",
                columns: table => new
                {
                    DegreeProgramId = table.Column<int>(type: "int", nullable: false),
                    ProgramCoursesCourseCode = table.Column<string>(type: "nvarchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDegreeProgram", x => new { x.DegreeProgramId, x.ProgramCoursesCourseCode });
                    table.ForeignKey(
                        name: "FK_CourseDegreeProgram_Courses_ProgramCoursesCourseCode",
                        column: x => x.ProgramCoursesCourseCode,
                        principalTable: "Courses",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseDegreeProgram_DegreePrograms_DegreeProgramId",
                        column: x => x.DegreeProgramId,
                        principalTable: "DegreePrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    EnrolledCoursesCourseCode = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    EnrolledStudentsUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.EnrolledCoursesCourseCode, x.EnrolledStudentsUserId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Courses_EnrolledCoursesCourseCode",
                        column: x => x.EnrolledCoursesCourseCode,
                        principalTable: "Courses",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Students_EnrolledStudentsUserId",
                        column: x => x.EnrolledStudentsUserId,
                        principalTable: "Students",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AcademicYears",
                columns: new[] { "Id", "IsDeleted", "Year" },
                values: new object[,]
                {
                    { 1, false, "First Year" },
                    { 2, false, "Second Year" },
                    { 3, false, "Third Year" },
                    { 4, false, "Fourth Year" }
                });

            migrationBuilder.InsertData(
                table: "DegreePrograms",
                columns: new[] { "Id", "IsDeleted", "Program" },
                values: new object[,]
                {
                    { 1, false, "B. Sc. Computer Science" },
                    { 2, false, "B. Sc. Telecommunications" },
                    { 3, false, "B. Sc. Computer Eng and IT" },
                    { 4, false, "B. Sc. Business and IT" },
                    { 5, false, "B. Sc. Electronic Science" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYearCourse_YearCoursesCourseCode",
                table: "AcademicYearCourse",
                column: "YearCoursesCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_Year",
                table: "AcademicYears",
                column: "Year",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseDegreeProgram_ProgramCoursesCourseCode",
                table: "CourseDegreeProgram",
                column: "ProgramCoursesCourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_EnrolledStudentsUserId",
                table: "CourseStudent",
                column: "EnrolledStudentsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseName",
                table: "Courses",
                column: "CourseName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LecturerUserId",
                table: "Courses",
                column: "LecturerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DegreePrograms_Program",
                table: "DegreePrograms",
                column: "Program",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_LecturerName",
                table: "Lecturers",
                column: "LecturerName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Name",
                table: "Students",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramId",
                table: "Students",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_YearId",
                table: "Students",
                column: "YearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicYearCourse");

            migrationBuilder.DropTable(
                name: "CourseDegreeProgram");

            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "DegreePrograms");
        }
    }
}
