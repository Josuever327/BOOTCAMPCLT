namespace Api.BootCamp.Api.Request;
public record PatchProductoRequest
{
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Precio { get; set; }
    public bool? Activo { get; set; }
    public int? CategoriaId { get; set; }
    public int? CantidadStock { get; set; }
}
