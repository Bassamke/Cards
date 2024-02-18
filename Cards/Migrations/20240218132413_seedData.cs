using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cards.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1924c084-b048-437a-81eb-14ee54412037", null, "Member", "MEMBER" },
                    { "c8437421-7e8a-4987-9cfe-75ea0f005cb3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7028cc46-7ca3-4e62-be1f-1b0951ec16a6", 0, "4409f290-abb2-4b19-aeb0-b5ced2da9c0c", "Admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAECGlVEtAM2WoeAmdSBKRwEVg8UcLr0KQhA+lpg3ZPb/+SE7AtPT9Euo2fyL9w0sX2A==", null, false, "060560fa-6f2d-4da6-a081-b66546829e96", false, "Admin@gmail.com" },
                    { "be932f2d-bcf7-41d5-a126-9565afdb20a3", 0, "2de7ea7a-259c-4982-80ef-9f0011ead6ac", "Member@gmail.com", false, false, null, "MEMBER@GMAIL.COM", "MEMBER@GMAIL.COM", "AQAAAAIAAYagAAAAEBb6238vkVASSBhuqCKSUQzYpVNpBY4y7gpihf9e04hLzANmDuIx0f38YoULiBhzbw==", null, false, "5edbfb08-b143-46ee-9202-ffe8e69a1724", false, "Member@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c8437421-7e8a-4987-9cfe-75ea0f005cb3", "7028cc46-7ca3-4e62-be1f-1b0951ec16a6" },
                    { "1924c084-b048-437a-81eb-14ee54412037", "be932f2d-bcf7-41d5-a126-9565afdb20a3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c8437421-7e8a-4987-9cfe-75ea0f005cb3", "7028cc46-7ca3-4e62-be1f-1b0951ec16a6" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1924c084-b048-437a-81eb-14ee54412037", "be932f2d-bcf7-41d5-a126-9565afdb20a3" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1924c084-b048-437a-81eb-14ee54412037");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c8437421-7e8a-4987-9cfe-75ea0f005cb3");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "7028cc46-7ca3-4e62-be1f-1b0951ec16a6");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "be932f2d-bcf7-41d5-a126-9565afdb20a3");
        }
    }
}
