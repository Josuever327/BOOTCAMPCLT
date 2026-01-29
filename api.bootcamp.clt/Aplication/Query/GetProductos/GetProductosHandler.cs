using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Interfaces;
using MediatR;

namespace Api.BootCamp.Aplication.Query.GetProductos
{
    public class GetProductosHandler : IRequestHandler<GetProductosQuery, IEnumerable<ProductoResponse>>
    {
        private readonly IProductoRepository _repository;
        private readonly ILogger<GetProductosHandler> _logger;

        public GetProductosHandler(IProductoRepository repository, ILogger<GetProductosHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductoResponse>> Handle(GetProductosQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo productos. CategoriaId={CategoriaId}", request.CategoriaId);

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
}
