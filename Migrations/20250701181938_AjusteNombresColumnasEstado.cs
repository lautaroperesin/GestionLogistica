using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AjusteNombresColumnasEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_envios_estados_envio_id_estado",
                table: "envios");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "vehiculos",
                newName: "estado_vehiculo");

            migrationBuilder.RenameColumn(
                name: "id_estado",
                table: "estados_envio",
                newName: "id_estado_envio");

            migrationBuilder.RenameColumn(
                name: "id_estado",
                table: "envios",
                newName: "id_estado_envio");

            migrationBuilder.RenameIndex(
                name: "IX_envios_id_estado",
                table: "envios",
                newName: "IX_envios_id_estado_envio");

            migrationBuilder.AddForeignKey(
                name: "FK_envios_estados_envio_id_estado_envio",
                table: "envios",
                column: "id_estado_envio",
                principalTable: "estados_envio",
                principalColumn: "id_estado_envio",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_envios_estados_envio_id_estado_envio",
                table: "envios");

            migrationBuilder.RenameColumn(
                name: "estado_vehiculo",
                table: "vehiculos",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "id_estado_envio",
                table: "estados_envio",
                newName: "id_estado");

            migrationBuilder.RenameColumn(
                name: "id_estado_envio",
                table: "envios",
                newName: "id_estado");

            migrationBuilder.RenameIndex(
                name: "IX_envios_id_estado_envio",
                table: "envios",
                newName: "IX_envios_id_estado");

            migrationBuilder.AddForeignKey(
                name: "FK_envios_estados_envio_id_estado",
                table: "envios",
                column: "id_estado",
                principalTable: "estados_envio",
                principalColumn: "id_estado",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
