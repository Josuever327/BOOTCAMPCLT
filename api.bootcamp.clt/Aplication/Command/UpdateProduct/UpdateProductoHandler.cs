using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Command.UpdateProduct;
using Api.BootCamp.Infrastructura.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.bootcamp.clt.Application.Commands.UpdateProducto;

public class UpdateProductoHandler : IRequestHandler<UpdateProductoCommand, ProductoResponse?>
{
    private readonly PostegresDbContext _context;

    public UpdateProductoHandler(PostegresDbContext context)
    {
        _context = context;
    }

    public async Task<ProductoResponse?> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Productos.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (entity is null)
            return null;

        entity.Codigo = request.Codigo;
        entity.Nombre = request.Nombre;
        entity.Descripcion = request.Descripcion;
        entity.Precio = request.Precio;
        entity.Activo = request.Activo;
        entity.CategoriaId = request.CategoriaId;
        entity.CantidadStock = request.CantidadStock;
        entity.FechaActualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new ProductoResponse(
            entity.Id,
            entity.Codigo,
            entity.Nombre,
            entity.Descripcion ?? string.Empty,
            entity.Precio,
            entity.Activo,
            entity.CategoriaId,
            entity.FechaCreacion,
            entity.FechaActualizacion,
            entity.CantidadStock
        );
    }
}