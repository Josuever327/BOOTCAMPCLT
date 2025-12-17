using Api.BootCamp.Infrastructura.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.BootCamp.Aplication.Command.DeleteProducto;

public class DeleteProductoHandler : IRequestHandler<DeleteProductoCommand, bool>
{
    private readonly PostegresDbContext _context;

    public DeleteProductoHandler(PostegresDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Productos
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (entity is null)
            return false;

        entity.Activo = false;
        entity.FechaActualizacion = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}