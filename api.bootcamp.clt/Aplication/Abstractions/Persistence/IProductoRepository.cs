using Api.BootCamp.Domain.Entity;

namespace Api.BootCamp.Aplication.Abstractions.Persistence
{
    public interface IProductoRepository
    {
        Task<bool> ExistsByCodigoAsync(
            string codigo,
            CancellationToken cancellationToken);

        Task AddAsync(
            Producto producto,
            CancellationToken cancellationToken);
    }
}
