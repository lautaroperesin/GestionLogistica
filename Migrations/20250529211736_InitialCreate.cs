using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cliente = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id_cliente);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "conductores",
                columns: table => new
                {
                    id_conductor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dni = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    conductor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clase_licencia = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vencimiento_licencia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    telefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conductores", x => x.id_conductor);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "estados_envio",
                columns: table => new
                {
                    id_estado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    estado = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados_envio", x => x.id_estado);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "paises",
                columns: table => new
                {
                    id_pais = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    pais = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codigo_iso = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paises", x => x.id_pais);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipos_carga",
                columns: table => new
                {
                    id_tipo_carga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    carga = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_carga", x => x.id_tipo_carga);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vehiculos",
                columns: table => new
                {
                    id_vehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    patente = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    marca = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    modelo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    refrigerado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    capacidad_kg = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ultima_inspeccion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehiculos", x => x.id_vehiculo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "provincias",
                columns: table => new
                {
                    id_provincia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    provincia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_pais = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provincias", x => x.id_provincia);
                    table.ForeignKey(
                        name: "FK_provincias_paises_id_pais",
                        column: x => x.id_pais,
                        principalTable: "paises",
                        principalColumn: "id_pais",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "localidades",
                columns: table => new
                {
                    id_localidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    localidad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_provincia = table.Column<int>(type: "int", nullable: false),
                    codigo_postal = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localidades", x => x.id_localidad);
                    table.ForeignKey(
                        name: "FK_localidades_provincias_id_provincia",
                        column: x => x.id_provincia,
                        principalTable: "provincias",
                        principalColumn: "id_provincia",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ubicaciones",
                columns: table => new
                {
                    id_ubicacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    direccion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_localidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ubicaciones", x => x.id_ubicacion);
                    table.ForeignKey(
                        name: "FK_ubicaciones_localidades_id_localidad",
                        column: x => x.id_localidad,
                        principalTable: "localidades",
                        principalColumn: "id_localidad",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "envios",
                columns: table => new
                {
                    id_envio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_origen = table.Column<int>(type: "int", nullable: false),
                    id_destino = table.Column<int>(type: "int", nullable: false),
                    fecha_creacion_envio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_salida_programada = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    fecha_salida_real = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    fecha_entrega_estimada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_entrega_real = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    id_estado = table.Column<int>(type: "int", nullable: false),
                    peso_kg = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    costo_total = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    id_vehiculo = table.Column<int>(type: "int", nullable: false),
                    id_conductor = table.Column<int>(type: "int", nullable: false),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_tipo_carga = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_envios", x => x.id_envio);
                    table.ForeignKey(
                        name: "FK_envios_clientes_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "clientes",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_envios_conductores_id_conductor",
                        column: x => x.id_conductor,
                        principalTable: "conductores",
                        principalColumn: "id_conductor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_envios_estados_envio_id_estado",
                        column: x => x.id_estado,
                        principalTable: "estados_envio",
                        principalColumn: "id_estado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_envios_tipos_carga_id_tipo_carga",
                        column: x => x.id_tipo_carga,
                        principalTable: "tipos_carga",
                        principalColumn: "id_tipo_carga",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_envios_ubicaciones_id_destino",
                        column: x => x.id_destino,
                        principalTable: "ubicaciones",
                        principalColumn: "id_ubicacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_envios_ubicaciones_id_origen",
                        column: x => x.id_origen,
                        principalTable: "ubicaciones",
                        principalColumn: "id_ubicacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_envios_vehiculos_id_vehiculo",
                        column: x => x.id_vehiculo,
                        principalTable: "vehiculos",
                        principalColumn: "id_vehiculo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "facturas",
                columns: table => new
                {
                    id_factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_envio = table.Column<int>(type: "int", nullable: false),
                    numero_factura = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_emision = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    monto_total = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    metodo_pago = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_estado_factura = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facturas", x => x.id_factura);
                    table.ForeignKey(
                        name: "FK_facturas_envios_id_envio",
                        column: x => x.id_envio,
                        principalTable: "envios",
                        principalColumn: "id_envio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_facturas_estados_factura_id_estado_factura",
                        column: x => x.id_estado_factura,
                        principalTable: "estados_factura",
                        principalColumn: "id_estado_factura",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "estados_envio",
                columns: new[] { "id_estado", "estado" },
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
                name: "IX_conductores_dni",
                table: "conductores",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_cliente",
                table: "envios",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_conductor",
                table: "envios",
                column: "id_conductor");

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_destino",
                table: "envios",
                column: "id_destino");

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_estado",
                table: "envios",
                column: "id_estado");

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_origen",
                table: "envios",
                column: "id_origen");

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_tipo_carga",
                table: "envios",
                column: "id_tipo_carga");

            migrationBuilder.CreateIndex(
                name: "IX_envios_id_vehiculo",
                table: "envios",
                column: "id_vehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_id_envio",
                table: "facturas",
                column: "id_envio");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_id_estado_factura",
                table: "facturas",
                column: "id_estado_factura");

            migrationBuilder.CreateIndex(
                name: "IX_facturas_numero_factura",
                table: "facturas",
                column: "numero_factura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_localidades_id_provincia",
                table: "localidades",
                column: "id_provincia");

            migrationBuilder.CreateIndex(
                name: "IX_paises_pais",
                table: "paises",
                column: "pais",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_provincias_id_pais",
                table: "provincias",
                column: "id_pais");

            migrationBuilder.CreateIndex(
                name: "IX_tipos_carga_carga",
                table: "tipos_carga",
                column: "carga",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ubicaciones_id_localidad",
                table: "ubicaciones",
                column: "id_localidad");

            migrationBuilder.CreateIndex(
                name: "IX_vehiculos_patente",
                table: "vehiculos",
                column: "patente",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "facturas");

            migrationBuilder.DropTable(
                name: "envios");

            migrationBuilder.DropTable(
                name: "estados_factura");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "conductores");

            migrationBuilder.DropTable(
                name: "estados_envio");

            migrationBuilder.DropTable(
                name: "tipos_carga");

            migrationBuilder.DropTable(
                name: "ubicaciones");

            migrationBuilder.DropTable(
                name: "vehiculos");

            migrationBuilder.DropTable(
                name: "localidades");

            migrationBuilder.DropTable(
                name: "provincias");

            migrationBuilder.DropTable(
                name: "paises");
        }
    }
}
