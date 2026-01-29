using Api.BootCamp.Api.Response;
using MediatR;

public record UpdateProductoCommand(
    int Id,
    string Codigo,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    bool Activo,
    int CategoriaId,
    int CantidadStock
) : IRequest<ProductoResponse?>
{
    public void Validate()
    {
        if (Id <= 0)
            throw new ArgumentException("Id inválido");

        if (Precio <= 0)
            throw new ArgumentException("Precio inválido");

        if (CategoriaId <= 0)
            throw new ArgumentException("Categoría inválida");
    }
}
