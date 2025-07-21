using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFechasEnvio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_entrega_estimada",
                table: "envios");

            migrationBuilder.DropColumn(
                name: "fecha_entrega_real",
                table: "envios");

            migrationBuilder.DropColumn(
                name: "fecha_salida_programada",
                table: "envios");

            migrationBuilder.RenameColumn(
                name: "fecha_salida_real",
                table: "envios",
                newName: "fecha_salida");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fecha_salida",
                table: "envios",
                newName: "fecha_salida_real");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_entrega_estimada",
                table: "envios",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_entrega_real",
                table: "envios",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_salida_programada",
                table: "envios",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
