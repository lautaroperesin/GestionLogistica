using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class EliminaTablaMetodoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movimientos_caja_metodos_pago_id_metodo_pago",
                table: "movimientos_caja");

            migrationBuilder.DropTable(
                name: "metodos_pago");

            migrationBuilder.DropIndex(
                name: "IX_movimientos_caja_id_metodo_pago",
                table: "movimientos_caja");

            migrationBuilder.RenameColumn(
                name: "id_metodo_pago",
                table: "movimientos_caja",
                newName: "metodo_pago");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "metodo_pago",
                table: "movimientos_caja",
                newName: "id_metodo_pago");

            migrationBuilder.CreateTable(
                name: "metodos_pago",
                columns: table => new
                {
                    id_metodo_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    metodo_pago = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metodos_pago", x => x.id_metodo_pago);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "metodos_pago",
                columns: new[] { "id_metodo_pago", "metodo_pago" },
                values: new object[,]
                {
                    { 1, "Efectivo" },
                    { 2, "Transferencia" },
                    { 3, "Cheque" },
                    { 4, "Tarjeta de Crédito" },
                    { 5, "Tarjeta de Débito" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_caja_id_metodo_pago",
                table: "movimientos_caja",
                column: "id_metodo_pago");

            migrationBuilder.AddForeignKey(
                name: "FK_movimientos_caja_metodos_pago_id_metodo_pago",
                table: "movimientos_caja",
                column: "id_metodo_pago",
                principalTable: "metodos_pago",
                principalColumn: "id_metodo_pago",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
