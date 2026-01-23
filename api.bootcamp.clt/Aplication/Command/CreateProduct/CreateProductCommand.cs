using MediatR;
using Api.BootCamp.Api.Response;

namespace Api.BootCamp.Aplication.Command.CreateProduct;

public record CreateProductoCommand(
    string Codigo,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    int CategoriaId,
    int CantidadStock
) : IRequest<ProductoResponse>;

/*Prueba commit*/