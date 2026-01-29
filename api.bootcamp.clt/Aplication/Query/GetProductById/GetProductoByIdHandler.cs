using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Interfaces;
using Api.BootCamp.Aplication.Query.GetProductById;
using MediatR;

namespace Api.BootCamp.Aplication.Query.GetProductoById
{
    public class GetProductoByIdHandler : IRequestHandler<GetProductoByIdQuery, ProductoResponse?>
    {
        private readonly IProductoRepository _repository;
        private readonly ILogger<GetProductoByIdHandler> _logger;

        public GetProductoByIdHandler(IProductoRepository repository, ILogger<GetProductoByIdHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ProductoResponse?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo producto por Id={Id}", request.Id);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null)
            {
                _logger.LogWarning("Producto no encontrado Id={Id}", request.Id);
                return null;
            }

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
}
