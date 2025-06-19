using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class ReemplazarEstadoFacturaPorEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_facturas_estados_factura_id_estado_factura",
                table: "facturas");

            migrationBuilder.DropTable(
                name: "estados_factura");

            migrationBuilder.DropIndex(
                name: "IX_facturas_id_estado_factura",
                table: "facturas");

            migrationBuilder.DropColumn(
                name: "id_estado_factura",
                table: "facturas");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "facturas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "facturas");

            migrationBuilder.AddColumn<int>(
                name: "id_estado_factura",
                table: "facturas",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "estados_factura",
                columns: table => new
                {
                    id_estado_factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    estado = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados_factura", x => x.id_estado_factura);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "estados_factura",
                columns: new[] { "id_estado_factura", "estado" },
                values: new object[,]
                {
                    { 1, "Emitida" },
                    { 2, "Pagada" },
                    { 3, "Vencida" },
                    { 4, "Anulada" },
                    { 5, "Parcialmente pagada" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_facturas_id_estado_factura",
                table: "facturas",
                column: "id_estado_factura");

            migrationBuilder.AddForeignKey(
                name: "FK_facturas_estados_factura_id_estado_factura",
                table: "facturas",
                column: "id_estado_factura",
                principalTable: "estados_factura",
                principalColumn: "id_estado_factura",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
