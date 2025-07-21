using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rol",
                table: "usuarios");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_expiracion_refresh_token",
                table: "usuarios",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "usuarios",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "rol_usuario",
                table: "usuarios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_expiracion_refresh_token",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "rol_usuario",
                table: "usuarios");

            migrationBuilder.AddColumn<int>(
                name: "rol",
                table: "usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
