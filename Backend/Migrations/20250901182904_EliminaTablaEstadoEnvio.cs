using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class EliminaTablaEstadoEnvio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_envios_estados_envio_id_estado_envio",
                table: "envios");

            migrationBuilder.DropTable(
                name: "estados_envio");

            migrationBuilder.DropIndex(
                name: "IX_envios_id_estado_envio",
                table: "envios");

            migrationBuilder.RenameColumn(
                name: "id_estado_envio",
                table: "envios",
                newName: "estado_envio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "estado_envio",
                table: "envios",
                newName: "id_estado_envio");

            migrationBuilder.CreateTable(
                name: "estados_envio",
                columns: table => new
                {
                    id_estado_envio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    estado = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados_envio", x => x.id_estado_envio);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "estados_envio",
                columns: new[] { "id_estado_envio", "estado" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "En preparación" },
                    { 3, "En tránsito" },
                    { 4, "Entregado" },
                    { 5, "Demorado" },
                    { 6, "Incidencia" },
                    { 7, "Cancelado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_estado_envio",
                table: "envios",
                column: "id_estado_envio");

            migrationBuilder.AddForeignKey(
                name: "FK_envios_estados_envio_id_estado_envio",
                table: "envios",
                column: "id_estado_envio",
                principalTable: "estados_envio",
                principalColumn: "id_estado_envio",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
