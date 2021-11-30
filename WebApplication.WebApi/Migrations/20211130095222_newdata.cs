using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.WebApi.Migrations
{
    public partial class newdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"), "cebca27c-f77b-4b58-bd44-9a2f8059c446", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"), new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ca5639f6-5c17-4b6f-8a80-c90de09a659b", "AQAAAAEAACcQAAAAEJnTG5lVTnOcRj+g9u3FksXVqrIshaCzgQnjh3QYCcN1brjBifFZIqXindyjbJ6cSg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("cc88ab6f-5d66-4c30-a60e-8f5254f1e112"), new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672") });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0027068e-4c5d-4ecb-a157-b9cc063cd672"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6aa3605-d632-40d2-9fe5-6073c31b708f", "AQAAAAEAACcQAAAAEA9NFLhGfPdW1GjBQXexvw8sebHaw/9F/FZWiuq7bIbN81pFOqKYtR6TzDWDu/g+7g==" });
        }
    }
}
