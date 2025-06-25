using Microsoft.AspNetCore.Mvc;
using PapeleriaInventary.Application.Services;
using PapeleriaInventary.Domain.Entities;
using ClosedXML;
using System.IO;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace PapeleriaInventary.Web.Controllers
{
    // Controlador para gestionar las operaciones CRUD de productos
    public class ProductsController : Controller
    {
        // Inyección de dependencia del servicio de productos
        private readonly IProductService _service;

        // Constructor que recibe el servicio de productos
        public ProductsController(IProductService service)
        {
            _service = service; // Asigna el servicio inyectado a la variable privada
        }

        public async Task<IActionResult> Index(string? search, string? category)
        {
            var products = await _service.GetAllAsync(search, category);
            return View(products); // Llama a Views/Products/Index.cshtml
        }

        // Acción GET para mostrar el formulario de creación de un producto
        public IActionResult Create()
        {
            // Retorna la vista vacía para crear un nuevo producto
            return View();
        }

        // Acción POST para procesar la creación de un producto
        [HttpPost]
        [ValidateAntiForgeryToken] // Previene ataques CSRF
        public async Task<IActionResult> Create(Product products)
        {
            // Valida el modelo recibido según las anotaciones de la entidad Product
            if (!ModelState.IsValid)
                // Si el modelo no es válido, retorna la vista con los datos ingresados para mostrar errores
                return View(products);

            // Llama al servicio para agregar el nuevo producto de forma asíncrona
            await _service.AddAsync(products);

            // Redirige a la acción Index (listado de productos) después de crear
            return RedirectToAction(nameof(Index));
        }

        // Acción GET para mostrar el formulario de edición de un producto existente
        public async Task<IActionResult> Edit(int id)
        {
            // Obtiene el producto por su id usando el servicio
            var product = await _service.GetByIdAsync(id);

            // Si no se encuentra el producto, retorna un error 404
            if (product == null)
                return NotFound();

            // Retorna la vista de edición con los datos del producto
            return View(product);
        }

        // Acción POST para procesar la edición de un producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            // Verifica que el id de la URL coincida con el id del producto recibido
            if (id != product.Id)
                return BadRequest();

            // Valida el modelo antes de actualizar
            if (!ModelState.IsValid)
                return View(product);

            // Llama al servicio para actualizar el producto de forma asíncrona
            await _service.UpdateAsync(product);

            // Redirige al listado de productos tras la actualización
            return RedirectToAction(nameof(Index));
        }

        // Acción GET para mostrar la confirmación de borrado de un producto
        public async Task<IActionResult> Delete(int id)
        {
            // Busca el producto por id
            var product = await _service.GetByIdAsync(id);

            // Si no existe, retorna 404
            if (product == null)
                return NotFound();

            // Muestra la vista de confirmación de borrado con los datos del producto
            return View(product);
        }

        // Acción POST para confirmar y ejecutar el borrado del producto
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Llama al servicio para eliminar el producto por id
            await _service.DeleteAsync(id);

            // Redirige al listado de productos tras eliminar
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Exporta la lista de productos filtrados (por búsqueda y categoría) a un archivo Excel (.xlsx).
        /// El archivo generado contiene las columnas: ID, Nombre, Categoría, Precio y Stock.
        /// </summary>
        /// <param name="search">Texto de búsqueda opcional para filtrar productos por nombre o descripción.</param>
        /// <param name="category">Categoría opcional para filtrar productos.</param>
        /// <returns>
        /// Un archivo Excel descargable con la información de los productos filtrados.
        /// El nombre del archivo incluye la fecha y hora de la exportación.
        /// </returns>
        public async Task<IActionResult> ExportToExcel(string? search, string? category)
        {
            var products = await _service.GetAllAsync(search, category);

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Productos");

            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Nombre";
            worksheet.Cell(1, 3).Value = "Categoría";
            worksheet.Cell(1, 4).Value = "Precio";
            worksheet.Cell(1, 5).Value = "Stock";

            int row = 2;

            foreach (var p in products)
            {
                worksheet.Cell(row, 1).Value = p.Id;
                worksheet.Cell(row, 2).Value = p.Name;
                worksheet.Cell(row, 3).Value = p.Category;
                worksheet.Cell(row, 4).Value = p.Price;
                worksheet.Cell(row, 5).Value = p.Stock;

                row++;
            }

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);
            stream.Position = 0;

            string fileName = $"Productos_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}