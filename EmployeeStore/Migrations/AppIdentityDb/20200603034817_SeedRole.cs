using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeStore.Migrations.AppIdentityDb
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "594f746e-5397-4003-ae92-4f9df68e0949", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "SuperAdmin", "84ec12cd-60cf-4989-b833-030b26a45382", "progmd@mail.ru", true, false, null, "progmd@mail.ru", "progmd@mail.ru", "AQAAAAEAACcQAAAAEAyma+KPOs/j65GniWma8HyRM4yZtVs6mzrj5Nah5IFdaXlsfyzujIc6h3OMLs3LiA==", null, false, "6fc32ff2-d296-4bd9-88fe-a0241dce9c99", false, "progmd@mail.ru" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
