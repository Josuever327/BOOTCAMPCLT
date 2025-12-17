using Api.BootCamp.Api.Response;
using Api.BootCamp.Domain.Entity;
using Api.BootCamp.Infrastructura.Context;
using MediatR;

namespace Api.BootCamp.Aplication.Command.CreateProduct;

public class CreateProductoHandler : IRequestHandler<CreateProductoCommand, ProductoResponse>
{
    private readonly PostegresDbContext _context;

    public CreateProductoHandler(PostegresDbContext context)
    {
        _context = context;
    }

    public async Task<ProductoResponse> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = new Producto
        {
            Codigo = request.Codigo,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio,
            CategoriaId = request.CategoriaId,
            CantidadStock = request.CantidadStock,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync(cancellationToken);

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