using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Interfaces;
using MediatR;

namespace Api.BootCamp.Aplication.Query.GetProductos;

public class GetProductosHandler : IRequestHandler<GetProductosQuery, IEnumerable<ProductoResponse>>
{
    private readonly IProductoRepository _repository;

    public GetProductosHandler(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductoResponse>> Handle(GetProductosQuery request, CancellationToken cancellationToken)
    {
        var productos = await _repository.GetAllAsync(request.CategoriaId, cancellationToken);

        return productos.Select(p => new ProductoResponse(
            p.Id,
            p.Codigo,
            p.Nombre,
            p.Descripcion ?? string.Empty,
            p.Precio,
            p.Activo,
            p.CategoriaId,
            p.FechaCreacion,
            p.FechaActualizacion,
            p.CantidadStock
        ));
    }
}
