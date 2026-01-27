using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Interfaces;
using Api.BootCamp.Aplication.Query.GetProductById;
using MediatR;

namespace Api.BootCamp.Aplication.Query.GetProductByHandler;

public class GetProductoByIdHandler : IRequestHandler<GetProductoByIdQuery, ProductoResponse?>
{
    private readonly IProductoRepository _repository;

    public GetProductoByIdHandler(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductoResponse?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

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
