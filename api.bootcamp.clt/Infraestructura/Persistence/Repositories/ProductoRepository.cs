using Api.BootCamp.Aplication.Abstractions.Persistence;
using Api.BootCamp.Domain.Entity;
using Api.BootCamp.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;

public class ProductoRepository : IProductoRepository
{
    private readonly PostegresDbContext _context;

    public ProductoRepository(PostegresDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByCodigoAsync(
        string codigo,
        CancellationToken cancellationToken)
    {
        return await _context.Productos
            .AnyAsync(p => p.Codigo == codigo, cancellationToken);
    }

    public async Task AddAsync(
        Producto producto,
        CancellationToken cancellationToken)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
