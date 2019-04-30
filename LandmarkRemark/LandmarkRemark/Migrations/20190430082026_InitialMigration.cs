using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LandmarkRemarks.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Remarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Remarks",
                columns: new[] { "Id", "DateCreated", "Latitude", "Longitude", "Note", "Username" },
                values: new object[] { new Guid("16e49139-9a7d-411c-92df-7843ffd462fb"), new DateTime(2019, 4, 30, 18, 20, 25, 942, DateTimeKind.Local).AddTicks(9010), 123.456, 456.78899999999999, "This is a test note", "test1@example.com" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "Username" },
                values: new object[] { new Guid("7fcca49d-40e0-447a-8bbf-3393b90c06a5"), "Test User 1", "uCLxzS3PxoW0foPjmAKJ/V2OP/OoLe8k19HWi7Jy6zI=", "test1@example.com" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "Username" },
                values: new object[] { new Guid("6eff546c-8112-4ead-8ce6-e77878ef7062"), "Test User 2", "eScFcq3tutWiEOJiTXbcfLnHn7Lu402/WcaX6VfMJKM=", "test2@example.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Remarks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
