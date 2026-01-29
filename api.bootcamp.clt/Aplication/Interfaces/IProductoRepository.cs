using Api.BootCamp.Domain.Entity;

namespace Api.BootCamp.Aplication.Interfaces
{
    public interface IProductoRepository
    {
        Task<Producto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Producto>> GetAllAsync(int? categoriaId, CancellationToken cancellationToken);
        Task AddAsync(Producto producto, CancellationToken cancellationToken);
        Task UpdateAsync(Producto producto, CancellationToken cancellationToken);
        Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken);
        Task<bool> CategoriaExisteAsync(int categoriaId, CancellationToken cancellationToken);
    }
}
