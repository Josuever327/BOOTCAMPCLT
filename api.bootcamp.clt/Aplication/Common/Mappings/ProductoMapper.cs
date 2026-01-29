using Api.BootCamp.Api.Response;
using Api.BootCamp.Domain.Entity;

namespace Api.BootCamp.Aplication.Common.Mappings
{
    public static class ProductoMapper
    {
        public static ProductoResponse ToResponse(this Producto producto)
        {
            return new ProductoResponse(
                producto.Id,
                producto.Codigo,
                producto.Nombre,
                producto.Descripcion ?? string.Empty,
                producto.Precio,
                producto.Activo,
                producto.CategoriaId,
                producto.FechaCreacion,
                producto.FechaActualizacion,
                producto.CantidadStock
            );
        }
    }
}
