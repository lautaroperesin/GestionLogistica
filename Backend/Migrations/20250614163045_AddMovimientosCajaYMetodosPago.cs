using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddMovimientosCajaYMetodosPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "metodo_pago",
                table: "facturas");

            migrationBuilder.DropColumn(
                name: "monto_total",
                table: "facturas");

            migrationBuilder.AlterColumn<string>(
                name: "numero_factura",
                table: "facturas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_vencimiento",
                table: "facturas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_cliente",
                table: "facturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "iva",
                table: "facturas",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "subtotal",
                table: "facturas",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "total",
                table: "facturas",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.CreateTable(
                name: "movimientos_caja",
                columns: table => new
                {
                    id_movimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_factura = table.Column<int>(type: "int", nullable: false),
                    fecha_pago = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    id_metodo_pago = table.Column<int>(type: "int", nullable: false),
                    observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
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
                    table.ForeignKey(
                        name: "FK_movimientos_caja_metodos_pago_id_metodo_pago",
                        column: x => x.id_metodo_pago,
                        principalTable: "metodos_pago",
                        principalColumn: "id_metodo_pago",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_facturas_id_cliente",
                table: "facturas",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_caja_id_factura",
                table: "movimientos_caja",
                column: "id_factura");

            migrationBuilder.CreateIndex(
                name: "IX_movimientos_caja_id_metodo_pago",
                table: "movimientos_caja",
                column: "id_metodo_pago");

            migrationBuilder.AddForeignKey(
                name: "FK_facturas_clientes_id_cliente",
                table: "facturas",
                column: "id_cliente",
                principalTable: "clientes",
                principalColumn: "id_cliente",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_facturas_clientes_id_cliente",
                table: "facturas");

            migrationBuilder.DropTable(
                name: "movimientos_caja");

            migrationBuilder.DropTable(
                name: "metodos_pago");

            migrationBuilder.DropIndex(
                name: "IX_facturas_id_cliente",
                table: "facturas");

            migrationBuilder.DropColumn(
                name: "fecha_vencimiento",
                table: "facturas");

            migrationBuilder.DropColumn(
                name: "id_cliente",
                table: "facturas");

            migrationBuilder.DropColumn(
                name: "iva",
                table: "facturas");

            migrationBuilder.DropColumn(
                name: "subtotal",
                table: "facturas");

            migrationBuilder.DropColumn(
                name: "total",
                table: "facturas");

            migrationBuilder.AlterColumn<string>(
                name: "numero_factura",
                table: "facturas",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "metodo_pago",
                table: "facturas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "monto_total",
                table: "facturas",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
