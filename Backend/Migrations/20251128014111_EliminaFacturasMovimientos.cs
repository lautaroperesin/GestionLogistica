using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class EliminaFacturasMovimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimientos_caja");

            migrationBuilder.DropTable(
                name: "facturas");

            migrationBuilder.DropColumn(
                name: "estado_vehiculo",
                table: "vehiculos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "estado_vehiculo",
                table: "vehiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "facturas",
                columns: table => new
                {
                    id_factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_envio = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    fecha_emision = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    iva = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    numero_factura = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subtotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facturas", x => x.id_factura);
                    table.ForeignKey(
                        name: "FK_facturas_clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facturas_envios_id_envio",
                        column: x => x.id_envio,
                        principalTable: "envios",
                        principalColumn: "id_envio",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movimientos_caja",
                columns: table => new
                {
                    id_movimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_factura = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    fecha_pago = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    metodo_pago = table.Column<int>(type: "int", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimientos_caja", x => x.id_movimiento);
                    table.ForeignKey(
                        name: "FK_movimientos_caja_facturas_id_factura",
                        column: x => x.id_factura,
                        principalTable: "facturas",
                        principalColumn: "id_factura",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_id_cliente",
                table: "facturas",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_id_envio",
                table: "facturas",
                column: "id_envio");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_numero_factura",
                table: "facturas",
                column: "numero_factura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_caja_id_factura",
                table: "movimientos_caja",
                column: "id_factura");
        }
    }
}
