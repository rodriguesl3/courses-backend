using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chama.Courses.Persistence.Migrations
{
    public partial class CalculateCourseAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseQueries");

            migrationBuilder.AddColumn<double>(
                name: "AverageAge",
                table: "Courses",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumAge",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumAge",
                table: "Courses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageAge",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MaximumAge",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MinimumAge",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseQueries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AverageAge = table.Column<double>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    MaximumAge = table.Column<int>(nullable: false),
                    MinimumAge = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseQueries_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseQueries_CourseId",
                table: "CourseQueries",
                column: "CourseId");
        }
    }
}
