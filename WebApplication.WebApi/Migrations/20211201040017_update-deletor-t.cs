using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.WebApi.Migrations
{
    public partial class updatedeletort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletorId",
                table: "CourseCategories");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                column: "ConcurrencyStamp",
                value: "381f77d0-2a59-42f1-8414-608f20b5e39d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f0304ccb-f14c-46dd-b712-8d4db19330fe", "AQAAAAEAACcQAAAAEFG7Mm2cXNV3nPaGTKQQuOGG72l2A+4LMtMy2XQIspKewZRWzK2xE9C7tdjhJFt4Jw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeletorId",
                table: "CourseCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}