using Api.BootCamp.Aplication.Interfaces;
using Api.BootCamp.Domain.Entity;
using Api.BootCamp.Infrastructura.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.BootCamp.Infrastructura.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly PostegresDbContext _context;

    public ProductoRepository(PostegresDbContext context)
    {
        _context = context;
    }

    public async Task<Producto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _context.Productos.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Producto>> GetAllAsync(int? categoriaId, CancellationToken cancellationToken)
    {
        var query = _context.Productos.AsNoTracking();

        if (categoriaId.HasValue)
            query = query.Where(p => p.CategoriaId == categoriaId.Value);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Producto producto, CancellationToken cancellationToken)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Producto producto, CancellationToken cancellationToken)
    {
        _context.Productos.Update(producto);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CategoriaExisteAsync(int categoriaId, CancellationToken cancellationToken)
        => await _context.Categorias.AnyAsync(c => c.Id == categoriaId, cancellationToken);
}
