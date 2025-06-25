using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapeleriaInventary.Infrastructure.Migrations
{
    /// <inheritdoc />
    // Clase parcial que representa la migración inicial de la base de datos
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        // Método que se ejecuta al aplicar la migración (crea la tabla Products)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Configura el conjunto de caracteres de la base de datos
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            // Crea la tabla Products con sus columnas y restricciones
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    // Columna Id: clave primaria, autoincremental
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    // Columna Name: nombre del producto, requerido, máximo 100 caracteres
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    // Columna Category: categoría del producto, requerido, máximo 100 caracteres
                    Category = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    // Columna Price: precio del producto, requerido, tipo decimal
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    // Columna Stock: cantidad en inventario, requerido
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    // Define la clave primaria de la tabla
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        // Método que se ejecuta al revertir la migración (elimina la tabla Products)
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
