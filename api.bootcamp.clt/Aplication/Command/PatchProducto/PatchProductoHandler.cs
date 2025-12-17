using Api.BootCamp.Api.Response;
using Api.BootCamp.Infrastructura.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.BootCamp.Aplication.Command.PatchProducto;

public class PatchProductoHandler : IRequestHandler<PatchProductoCommand, ProductoResponse?>
{
    private readonly PostegresDbContext _context;

    public PatchProductoHandler(PostegresDbContext context)
    {
        _context = context;
    }

    public async Task<ProductoResponse?> Handle( PatchProductoCommand request,  CancellationToken cancellationToken)
    {
        var entity = await _context.Productos
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (entity is null)
            return null;

        if (request.Codigo is not null)
            entity.Codigo = request.Codigo;

        if (request.Nombre is not null)
            entity.Nombre = request.Nombre;

        if (request.Descripcion is not null)
            entity.Descripcion = request.Descripcion;

        if (request.Precio.HasValue)
            entity.Precio = request.Precio.Value;

        if (request.Activo.HasValue)
            entity.Activo = request.Activo.Value;

        if (request.CategoriaId.HasValue)
            entity.CategoriaId = request.CategoriaId.Value;

        if (request.CantidadStock.HasValue)
            entity.CantidadStock = request.CantidadStock.Value;

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