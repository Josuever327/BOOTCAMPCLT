using MediatR;
using Microsoft.EntityFrameworkCore;
using Api.BootCamp.Api.Response;
using Api.BootCamp.Infraestructura.Context;
using Api.BootCamp.Aplication.Query.GetProductById;

namespace Api.BootCamp.Aplication.Query.GetProductByHandler;

public class GetProductoByIdHandler : IRequestHandler<GetProductoByIdQuery, ProductoResponse?>
{
    private readonly PostegresDbContext _context;

    public GetProductoByIdHandler(PostegresDbContext context)
    {
        _context = context;
    }

    public async Task<ProductoResponse?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Productos.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (entity is null)
            return null;

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
