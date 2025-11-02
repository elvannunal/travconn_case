using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("c9336243-d5bd-46f3-8b8f-cc754afbc398"), "user3@gmail.com", "User3 LastName", "User3 Lastname", "AQAAAAIAAYagAAAAEEGYGA/9/8GlcunfYl7L4pC2YZ6t70pU9zoMhp8x4D3BT8yfalIPYP8cnI3jvHCzbw==", "User" },
                    { new Guid("dfa86cdf-de8a-469a-801d-eda786b908b6"), "user2@gmail.com", "User2 LastName", "User2 Lastname", "AQAAAAIAAYagAAAAEE5Ghw4I+CNGzhnLBuxGjNd31hCoJhPVbAldgwEuDR+8txFzStAdmlifNcImm6tJdQ==", "User" },
                    { new Guid("f7be2cea-9450-47ce-b47c-2765db336eb1"), "user@gmail.com", "User1 LastName", "User1 Lastname", "AQAAAAIAAYagAAAAEJtnVaDv1S4qlEXx1npQMw46zdqk9ffwKhUdeMIVZbOMcZPQ4fhkOn1BmzjKVV+79A==", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
