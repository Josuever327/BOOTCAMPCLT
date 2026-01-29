using Api.BootCamp.Api.Response;
using Api.BootCamp.Domain.Entity;
using Api.BootCamp.Aplication.Abstractions.Persistence;
using Api.BootCamp.Aplication.Abstractions.Common;
using Api.BootCamp.Aplication.Common.Exceptions;
using Api.BootCamp.Aplication.Common.Mappings;
using MediatR;

namespace Api.BootCamp.Aplication.Command.CreateProduct;

public class CreateProductoHandler
    : IRequestHandler<CreateProductoCommand, ProductoResponse>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IDateTimeProvider _dateTime;

    public CreateProductoHandler(
        IProductoRepository productoRepository,
        IDateTimeProvider dateTime)
    {
        _productoRepository = productoRepository;
        _dateTime = dateTime;
    }

    public async Task<ProductoResponse> Handle(
        CreateProductoCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Precio <= 0)
            throw new DomainException("El precio debe ser mayor a cero.");

        if (await _productoRepository.ExistsByCodigoAsync(
            request.Codigo, cancellationToken))
        {
            throw new DomainException("Ya existe un producto con ese código.");
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
            FechaCreacion = _dateTime.UtcNow
        };

        await _productoRepository.AddAsync(producto, cancellationToken);

        return producto.ToResponse();
    }
}
