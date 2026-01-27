using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Interfaces;
using Api.BootCamp.Domain.Entity;
using MediatR;

namespace Api.BootCamp.Aplication.Command.CreateProduct;

public class CreateProductoHandler : IRequestHandler<CreateProductoCommand, ProductoResponse>
{
    private readonly IProductoRepository _repository;
    private readonly ILogger<CreateProductoHandler> _logger;

    public CreateProductoHandler(
        IProductoRepository repository,
        ILogger<CreateProductoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ProductoResponse> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
    {
        request.Validate();
        _logger.LogInformation(
    "Creando producto Codigo={Codigo}, CategoriaId={CategoriaId}",
    request.Codigo,
    request.CategoriaId
);

        var categoriaExiste = await _repository
            .CategoriaExisteAsync(request.CategoriaId, cancellationToken);

        if (!categoriaExiste)
            throw new ArgumentException("La categoría no existe");

        var producto = new Producto
        {
            Codigo = request.Codigo,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio,
            CategoriaId = request.CategoriaId,
            CantidadStock = request.CantidadStock,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        await _repository.AddAsync(producto, cancellationToken);

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
