using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Command.CreateProduct;
using Api.BootCamp.Aplication.Interfaces;
using Api.BootCamp.Domain.Entity;
using MediatR;

namespace Api.BootCamp.Aplication.Command.CreateProducto
{
    public class CreateProductoHandler : IRequestHandler<CreateProductoCommand, ProductoResponse>
    {
        private readonly IProductoRepository _repository;
        private readonly ILogger<CreateProductoHandler> _logger;

        public CreateProductoHandler(IProductoRepository repository, ILogger<CreateProductoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ProductoResponse> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (request.Precio <= 0)
            {
                _logger.LogWarning("Intento de crear producto con precio <= 0 Codigo={Codigo}", request.Codigo);
                throw new ArgumentException("El precio debe ser mayor a cero.");
            }

            if (await _repository.ExistsByCodigoAsync(request.Codigo, cancellationToken))
            {
                _logger.LogWarning("Intento de crear producto con código duplicado Codigo={Codigo}", request.Codigo);
                throw new ArgumentException("Ya existe un producto con ese código.");
            }

            var categoriaExiste = await _repository.CategoriaExisteAsync(request.CategoriaId, cancellationToken);
            if (!categoriaExiste)
            {
                _logger.LogWarning("Intento de crear producto con categoria inexistente CategoriaId={CategoriaId}", request.CategoriaId);
                throw new ArgumentException("La categoría no existe");
            }

            var producto = new Producto
            {
                Codigo = request.Codigo,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Precio = request.Precio,
                CategoriaId = request.CategoriaId,
                CantidadStock = request.CantidadStock,
                Activo = true,
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow
            };

            await _repository.AddAsync(producto, cancellationToken);

            _logger.LogInformation("Producto creado Codigo={Codigo}, Id={Id}", producto.Codigo, producto.Id);

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
}
