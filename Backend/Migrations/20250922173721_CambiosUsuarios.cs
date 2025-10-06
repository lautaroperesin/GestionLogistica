using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class CambiosUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_expiracion_refresh_token",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "ultimo_acceso",
                table: "usuarios");

            migrationBuilder.RenameColumn(
                name: "rol_usuario",
                table: "usuarios",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "usuarios",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "fecha_alta",
                table: "usuarios",
                newName: "FechaRegistracion");

            migrationBuilder.RenameColumn(
                name: "activo",
                table: "usuarios",
                newName: "isDeleted");

            migrationBuilder.AddColumn<string>(
                name: "Dni",
                table: "usuarios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Domicilio",
                table: "usuarios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "usuarios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TipoRol",
                table: "usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dni",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "Domicilio",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "usuarios");

            migrationBuilder.DropColumn(
                name: "TipoRol",
                table: "usuarios");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "usuarios",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "usuarios",
                newName: "activo");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "usuarios",
                newName: "rol_usuario");

            migrationBuilder.RenameColumn(
                name: "FechaRegistracion",
                table: "usuarios",
                newName: "fecha_alta");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "ultimo_acceso",
                table: "usuarios",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
