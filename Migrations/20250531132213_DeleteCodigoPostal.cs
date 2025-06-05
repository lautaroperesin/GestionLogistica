using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticaBackend.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCodigoPostal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigo_postal",
                table: "localidades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigo_postal",
                table: "localidades",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
