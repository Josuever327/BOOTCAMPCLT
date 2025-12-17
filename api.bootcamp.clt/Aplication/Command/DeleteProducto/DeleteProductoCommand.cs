using MediatR;

namespace Api.BootCamp.Aplication.Command.DeleteProducto;

public record DeleteProductoCommand(int Id) : IRequest<bool>;