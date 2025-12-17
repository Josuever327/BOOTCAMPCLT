namespace Api.BootCamp.Api.Response;

public record ProductoResponse(
    int Id,
    string Codigo,
    string Nombre,
    string Descripcion,
    decimal Precio,
    bool Activo,
    int CategoriaId,
    DateTime FechaCreacion,
    DateTime? FechaActualizacion,
    int CantidadStock
);