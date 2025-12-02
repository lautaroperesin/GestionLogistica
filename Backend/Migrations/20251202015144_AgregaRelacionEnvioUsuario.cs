using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AgregaRelacionEnvioUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_conductor",
                table: "envios",
                newName: "id_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_envios_id_conductor",
                table: "envios",
                newName: "IX_envios_id_usuario");


            migrationBuilder.AddForeignKey(
                name: "FK_envios_usuarios_id_usuario",
                table: "envios",
                column: "id_usuario",
                principalTable: "usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_envios_conductores_ConductorIdConductor",
                table: "envios");

            migrationBuilder.DropForeignKey(
                name: "FK_envios_usuarios_id_usuario",
                table: "envios");

            migrationBuilder.DropIndex(
                name: "IX_envios_ConductorIdConductor",
                table: "envios");

            migrationBuilder.DropColumn(
                name: "ConductorIdConductor",
                table: "envios");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "envios",
                newName: "id_conductor");

            migrationBuilder.RenameIndex(
                name: "IX_envios_id_usuario",
                table: "envios",
                newName: "IX_envios_id_conductor");
        }
    }
}
