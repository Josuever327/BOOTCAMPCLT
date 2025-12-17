namespace Api.BootCamp.Domain.Entity;

public class Categorias
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string? Descripcion { get; set; }
    public bool Estado { get; set; } = true;

}
