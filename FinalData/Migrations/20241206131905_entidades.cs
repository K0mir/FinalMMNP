using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalData.Migrations
{
    /// <inheritdoc />
    public partial class entidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoDeporte",
                columns: table => new
                {
                    IdTipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeporte", x => x.IdTipo);
                });

            migrationBuilder.CreateTable(
                name: "Deporte",
                columns: table => new
                {
                    DeporteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantJugadores = table.Column<int>(type: "int", nullable: false),
                    FechaCracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Popularidad = table.Column<int>(type: "int", nullable: false),
                    IdTipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deporte", x => x.DeporteId);
                    table.ForeignKey(
                        name: "FK_Deporte_TipoDeporte_IdTipo",
                        column: x => x.IdTipo,
                        principalTable: "TipoDeporte",
                        principalColumn: "IdTipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deporte_IdTipo",
                table: "Deporte",
                column: "IdTipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deporte");

            migrationBuilder.DropTable(
                name: "TipoDeporte");
        }
    }
}
