using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Accounts",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c9336243-d5bd-46f3-8b8f-cc754afbc398"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEClk2HrEYI+xitJhIdqQSwwYzTtVSYuomNHaGlghbPINTbtYs+YWQL+92apPJq7znw==");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dfa86cdf-de8a-469a-801d-eda786b908b6"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEuqy+k25QmeoyCc3HtlY640H/tpv7uJoiNAfqOX9EN8LVdOjGSmFdp9JJtru8mjbg==");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f7be2cea-9450-47ce-b47c-2765db336eb1"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOgm3hO3Iw42LvL7Oz0GDaPpHIyi5Luk1qxrFwvFot+fMVW3RsKb9budLYq2h1yx7Q==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("c9336243-d5bd-46f3-8b8f-cc754afbc398"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEGYGA/9/8GlcunfYl7L4pC2YZ6t70pU9zoMhp8x4D3BT8yfalIPYP8cnI3jvHCzbw==");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("dfa86cdf-de8a-469a-801d-eda786b908b6"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEE5Ghw4I+CNGzhnLBuxGjNd31hCoJhPVbAldgwEuDR+8txFzStAdmlifNcImm6tJdQ==");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("f7be2cea-9450-47ce-b47c-2765db336eb1"),
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJtnVaDv1S4qlEXx1npQMw46zdqk9ffwKhUdeMIVZbOMcZPQ4fhkOn1BmzjKVV+79A==");
        }
    }
}
