using Api.BootCamp.Api.Response;
using Api.BootCamp.Infrastructura.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.BootCamp.Aplication.Query.GetProductos;

public class GetProductosHandler : IRequestHandler<GetProductosQuery, IEnumerable<ProductoResponse>>
{
    private readonly PostegresDbContext _context;

    public GetProductosHandler(PostegresDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductoResponse>> Handle(GetProductosQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Productos.AsNoTracking();

        if (request.CategoriaId.HasValue)
            query = query.Where(p => p.CategoriaId == request.CategoriaId.Value);

        return await query
            .Select(p => new ProductoResponse(
                p.Id,
                p.Codigo,
                p.Nombre,
                p.Descripcion ?? string.Empty,
                p.Precio,
                p.Activo,
                p.CategoriaId,
                p.FechaCreacion,
                p.FechaActualizacion,
                p.CantidadStock))
            .ToListAsync(cancellationToken);
    }
}