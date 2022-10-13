using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpelVagtplan.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medarbejder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medarbejder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vagt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedarbejderId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vagt_Medarbejder_MedarbejderId",
                        column: x => x.MedarbejderId,
                        principalTable: "Medarbejder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opgave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgeLimit = table.Column<int>(type: "int", nullable: false),
                    vagtId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opgave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opgave_Vagt_vagtId",
                        column: x => x.vagtId,
                        principalTable: "Vagt",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opgave_vagtId",
                table: "Opgave",
                column: "vagtId");

            migrationBuilder.CreateIndex(
                name: "IX_Vagt_MedarbejderId",
                table: "Vagt",
                column: "MedarbejderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opgave");

            migrationBuilder.DropTable(
                name: "Vagt");

            migrationBuilder.DropTable(
                name: "Medarbejder");
        }
    }
}
