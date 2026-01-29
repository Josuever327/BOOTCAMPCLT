using Api.BootCamp.Aplication.Interfaces;
using Api.BootCamp.Domain.Entity;
using Api.BootCamp.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.BootCamp.Infraestructura.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly PostegresDbContext _context;

        public ProductoRepository(PostegresDbContext context)
        {
            _context = context;
        }

        public async Task<Producto?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await _context.Productos
                             .AsNoTracking()
                             .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<IEnumerable<Producto>> GetAllAsync(int? categoriaId, CancellationToken cancellationToken)
        {
            IQueryable<Producto> query = _context.Productos.AsNoTracking();

            if (categoriaId.HasValue)
                query = query.Where(p => p.CategoriaId == categoriaId.Value);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Producto producto, CancellationToken cancellationToken)
        {
            await _context.Productos.AddAsync(producto, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Producto producto, CancellationToken cancellationToken)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> CategoriaExisteAsync(int categoriaId, CancellationToken cancellationToken)
        {
            return await _context.Categorias
                                 .AsNoTracking()
                                 .AnyAsync(c => c.Id == categoriaId, cancellationToken);
        }

        public async Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken)
        {
            return await _context.Productos
                                 .AsNoTracking()
                                 .AnyAsync(p => p.Codigo == codigo, cancellationToken);
        }
    }
}
