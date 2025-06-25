using System.ComponentModel.DataAnnotations;


namespace PapeleriaInventary.Domain.Entities
{
    // Representa un producto en el inventario de la papelería
    public class Product
    {
        // Identificador único del producto
        public int Id { get; set; }

        // Nombre del producto (obligatorio, máximo 100 caracteres)
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Categoría del producto (obligatorio, máximo 100 caracteres)
        [Required]
        [MaxLength(100)]
        public string Category { get; set; }

        // Precio del producto (debe ser mayor a 0)
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        // Cantidad disponible en inventario (no puede ser negativa)
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}
