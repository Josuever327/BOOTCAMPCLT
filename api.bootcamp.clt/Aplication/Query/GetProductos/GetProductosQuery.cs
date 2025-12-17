using Api.BootCamp.Api.Response;
using MediatR;

namespace Api.BootCamp.Aplication.Query.GetProductos;

public record GetProductosQuery(int? CategoriaId) : IRequest<IEnumerable<ProductoResponse>>;
