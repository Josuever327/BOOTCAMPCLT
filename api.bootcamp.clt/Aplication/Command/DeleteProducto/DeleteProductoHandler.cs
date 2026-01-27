using Api.BootCamp.Aplication.Interfaces;
using MediatR;

namespace Api.BootCamp.Aplication.Command.DeleteProducto;

public class DeleteProductoHandler : IRequestHandler<DeleteProductoCommand, bool>
{
    private readonly IProductoRepository _repository;

    private readonly ILogger<DeleteProductoHandler> _logger;

    public DeleteProductoHandler(
        IProductoRepository repository,
        ILogger<DeleteProductoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _logger.LogInformation(
    "Eliminando producto Id={Id}",
    request.Id
);
        if (entity is null)
        {
            _logger.LogWarning(
                "Delete fallido. Producto no encontrado Id={Id}",
                request.Id
            );
            return false;
        }
;

        entity.Activo = false;
        entity.FechaActualizacion = DateTime.UtcNow;

        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
