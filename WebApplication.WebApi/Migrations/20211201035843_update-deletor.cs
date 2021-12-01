using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.WebApi.Migrations
{
    public partial class updatedeletor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletorId",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "DeletorId",
                table: "UserClasses");

            migrationBuilder.DropColumn(
                name: "DeletorId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "DeletorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "CourseCategories");

            migrationBuilder.DropColumn(
                name: "DeletorId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "DeletorId",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                column: "ConcurrencyStamp",
                value: "105165a1-ead3-43b0-aa49-17f425bc1dfb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "95380ca4-7ece-4f60-bb7f-d4eacc754277", "AQAAAAEAACcQAAAAEOezFFSGe7J5VXYv4C7J0ogCsnFME6HWFCVtg4QbKqmEzFEfwKiHOOounX6XJp9Hcg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeletorId",
                table: "UserCourses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletorId",
                table: "UserClasses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletorId",
                table: "Topics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletorId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdaterId",
                table: "CourseCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletorId",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletorId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                column: "ConcurrencyStamp",
                value: "ab80aadf-5170-43fa-9677-3067474c9e50");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "01bd89ee-55c4-4af2-97ac-053c9ebedbc2", "AQAAAAEAACcQAAAAEE4YnW+XXxuWHM3+Z+2Bcx1eQ1EPeSNMiXb3vI6reRjeEUkZO8KUuRhFvm2sQ/a0Dw==" });
        }
    }
}
