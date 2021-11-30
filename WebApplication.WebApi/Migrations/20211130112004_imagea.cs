using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.WebApi.Migrations
{
    public partial class imagea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Topics",
                type: "nvarchar(max)",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Topics");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"),
                column: "ConcurrencyStamp",
                value: "3f183263-573b-4e90-ae0a-6a4d5a04d5a6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "165124c3-904f-4875-9199-95a103c56270", "AQAAAAEAACcQAAAAEKr4kLleiaL+00FAn5Ubm7Z89uQCrS995PYL1n9ZKXan7z7/Uy19iY1+F7XXcPs1Xg==" });
        }
    }
}
