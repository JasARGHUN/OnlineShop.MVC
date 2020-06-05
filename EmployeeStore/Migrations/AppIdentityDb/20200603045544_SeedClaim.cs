using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeStore.Migrations.AppIdentityDb
{
    public partial class SeedClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "923d8923-35af-4e15-a6bb-eb790dcef5e0");

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 125, "Create Role", "Create Role", "1" },
                    { 135, "Edit Role", "Edit Role", "1" },
                    { 145, "Delete Role", "Delete Role", "1" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7500cfa7-f8ee-4ed3-a468-60177291ac66", "AQAAAAEAACcQAAAAEFamG2jejaB8UVX+x/9KmNQajWt0sVg9i7jvYdzIRcZkzlWbj5yxINV39zrAIUMxAA==", "848bc8c0-512c-4bbd-8668-ba3fc874ed27" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "594f746e-5397-4003-ae92-4f9df68e0949");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84ec12cd-60cf-4989-b833-030b26a45382", "AQAAAAEAACcQAAAAEAyma+KPOs/j65GniWma8HyRM4yZtVs6mzrj5Nah5IFdaXlsfyzujIc6h3OMLs3LiA==", "6fc32ff2-d296-4bd9-88fe-a0241dce9c99" });
        }
    }
}
