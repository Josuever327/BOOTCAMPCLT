using MediatR;
using Api.BootCamp.Api.Response;

namespace Api.BootCamp.Aplication.Query.GetProductById;

public record GetProductoByIdQuery(int Id) : IRequest<ProductoResponse?>;
