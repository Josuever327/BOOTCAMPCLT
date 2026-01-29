using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Interfaces;
using MediatR;

namespace Api.BootCamp.Aplication.Command.PatchProducto
{
    public class PatchProductoHandler : IRequestHandler<PatchProductoCommand, ProductoResponse?>
    {
        private readonly IProductoRepository _repository;
        private readonly ILogger<PatchProductoHandler> _logger;

        public PatchProductoHandler(
            IProductoRepository repository,
            ILogger<PatchProductoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ProductoResponse?> Handle(PatchProductoCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            _logger.LogInformation("Patch producto Id={Id}", request.Id);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
            {
                _logger.LogWarning("Patch fallido. Producto no encontrado Id={Id}", request.Id);
                return null;
            }

            if (request.CategoriaId.HasValue)
            {
                bool categoriaExiste = await _repository
                    .CategoriaExisteAsync(request.CategoriaId.Value, cancellationToken);

                if (!categoriaExiste)
                {
                    _logger.LogWarning("Categoria Id={CategoriaId} no existe", request.CategoriaId.Value);
                    throw new ArgumentException("La categoría no existe");
                }

                entity.CategoriaId = request.CategoriaId.Value;
            }

            if (request.Codigo is not null) entity.Codigo = request.Codigo;
            if (request.Nombre is not null) entity.Nombre = request.Nombre;
            if (request.Descripcion is not null) entity.Descripcion = request.Descripcion;
            if (request.Precio.HasValue) entity.Precio = request.Precio.Value;
            if (request.Activo.HasValue) entity.Activo = request.Activo.Value;
            if (request.CantidadStock.HasValue) entity.CantidadStock = request.CantidadStock.Value;

            entity.FechaActualizacion = DateTime.UtcNow;

            await _repository.UpdateAsync(entity, cancellationToken);

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
