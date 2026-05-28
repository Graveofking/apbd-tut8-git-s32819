using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APBD7.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentManufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Abbreviation = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentManufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Abbreviation = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Warranty = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ComponentManufacturersId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComponentTypesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Components_ComponentManufacturers_ComponentManufacturersId",
                        column: x => x.ComponentManufacturersId,
                        principalTable: "ComponentManufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Components_ComponentTypes_ComponentTypesId",
                        column: x => x.ComponentTypesId,
                        principalTable: "ComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PCComponents",
                columns: table => new
                {
                    PCId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComponentCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCComponents", x => new { x.PCId, x.ComponentCode });
                    table.ForeignKey(
                        name: "FK_PCComponents_Components_ComponentCode",
                        column: x => x.ComponentCode,
                        principalTable: "Components",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCComponents_PCs_PCId",
                        column: x => x.PCId,
                        principalTable: "PCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ComponentManufacturers",
                columns: new[] { "Id", "Abbreviation", "FoundationDate", "FullName" },
                values: new object[,]
                {
                    { 1, "INT", new DateTime(1968, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intel Corporation" },
                    { 2, "AMD", new DateTime(1969, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced Micro Devices, Inc." },
                    { 3, "NVD", new DateTime(1993, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "NVIDIA Corporation" }
                });

            migrationBuilder.InsertData(
                table: "ComponentTypes",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, "CPU", "Central Processing Unit" },
                    { 2, "GPU", "Graphics Processing Unit" },
                    { 3, "RAM", "Random Access Memory" }
                });

            migrationBuilder.InsertData(
                table: "PCs",
                columns: new[] { "Id", "CreatedAt", "Name", "Stock", "Warranty", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), "Gaming Beast X", 5, 36, 12.5 },
                    { 2, new DateTime(2026, 4, 15, 13, 30, 0, 0, DateTimeKind.Unspecified), "Office Mini Pro", 12, 24, 4.2000000000000002 },
                    { 3, new DateTime(2026, 3, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Workstation Ultra", 3, 48, 18.0 }
                });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Code", "ComponentManufacturersId", "ComponentTypesId", "Description", "Name" },
                values: new object[,]
                {
                    { "I9-13900K", 1, 1, "24-core desktop processor", "Intel Core i9-13900K" },
                    { "RTX-4090", 3, 2, "24GB GDDR6X graphics card", "NVIDIA GeForce RTX 4090" },
                    { "RX-7900XT", 2, 2, "20GB GDDR6 graphics card", "AMD Radeon RX 7900 XT" }
                });

            migrationBuilder.InsertData(
                table: "PCComponents",
                columns: new[] { "ComponentCode", "PCId", "Amount" },
                values: new object[,]
                {
                    { "I9-13900K", 1, 1 },
                    { "RTX-4090", 1, 1 },
                    { "RX-7900XT", 2, 1 },
                    { "I9-13900K", 3, 2 },
                    { "RTX-4090", 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentManufacturersId",
                table: "Components",
                column: "ComponentManufacturersId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypesId",
                table: "Components",
                column: "ComponentTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_PCComponents_ComponentCode",
                table: "PCComponents",
                column: "ComponentCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PCComponents");
            migrationBuilder.DropTable(name: "Components");
            migrationBuilder.DropTable(name: "PCs");
            migrationBuilder.DropTable(name: "ComponentManufacturers");
            migrationBuilder.DropTable(name: "ComponentTypes");
        }
    }
}
