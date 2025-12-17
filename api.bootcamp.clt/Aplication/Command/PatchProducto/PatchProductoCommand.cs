using Api.BootCamp.Api.Response;
using MediatR;

namespace Api.BootCamp.Aplication.Command.PatchProducto;

public record PatchProductoCommand(
    int Id,
    string? Codigo,
    string? Nombre,
    string? Descripcion,
    decimal? Precio,
    bool? Activo,
    int? CategoriaId,
    int? CantidadStock
) : IRequest<ProductoResponse?>;
