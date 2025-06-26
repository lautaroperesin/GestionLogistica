using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoCamposEnEnviosYVehiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "estado",
                table: "vehiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "rto_vencimiento",
                table: "vehiculos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "numero_seguimiento",
                table: "envios",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estado",
                table: "vehiculos");

            migrationBuilder.DropColumn(
                name: "rto_vencimiento",
                table: "vehiculos");

            migrationBuilder.DropColumn(
                name: "numero_seguimiento",
                table: "envios");
        }
    }
}
