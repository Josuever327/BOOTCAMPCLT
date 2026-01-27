using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Interfaces;
using MediatR;

namespace api.bootcamp.clt.Application.Commands.UpdateProducto;

public class UpdateProductoHandler : IRequestHandler<UpdateProductoCommand, ProductoResponse?>
{
    private readonly IProductoRepository _repository;

    private readonly ILogger<UpdateProductoHandler> _logger;

    public UpdateProductoHandler(
        IProductoRepository repository,
        ILogger<UpdateProductoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<ProductoResponse?> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
    {
        request.Validate();
        _logger.LogInformation(
    "Actualizando producto Id={Id}",
    request.Id
);

        var categoriaExiste = await _repository
            .CategoriaExisteAsync(request.CategoriaId, cancellationToken);

        if (!categoriaExiste)
            throw new ArgumentException("La categoría no existe");

        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
      
        if (entity is null)
        {
            _logger.LogWarning(
                "Producto no encontrado. Id={Id}",
                request.Id
            );
            return null;
        }


        entity.Codigo = request.Codigo;
        entity.Nombre = request.Nombre;
        entity.Descripcion = request.Descripcion;
        entity.Precio = request.Precio;
        entity.Activo = request.Activo;
        entity.CategoriaId = request.CategoriaId;
        entity.CantidadStock = request.CantidadStock;
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
