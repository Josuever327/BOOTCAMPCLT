using Api.BootCamp.Api.Request;
using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Command.CreateProduct;
using Api.BootCamp.Aplication.Command.DeleteProducto;
using Api.BootCamp.Aplication.Query.GetProductById;
using Api.BootCamp.Aplication.Query.GetProductos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.BootCamp.Api.Controllers;

[ApiController]
[Route("api/v1/productos")]
public class ProductosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromQuery] int? categoriaId)
    {
        var result = await _mediator.Send(new GetProductosQuery(categoriaId));

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetProductoByIdQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductoRequest request)
    {
        var command = new CreateProductoCommand(
            request.Codigo,
            request.Nombre,
            request.Descripcion,
            request.Precio,
            request.CategoriaId,
            request.CantidadStock
        );

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }


    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UpdateProductoRequest request)
    {
        var command = new UpdateProductoCommand(
            id,
            request.Codigo,
            request.Nombre,
            request.Descripcion,
            request.Precio,
            request.Activo,
            request.CategoriaId,
            request.CantidadStock
        );

        var result = await _mediator.Send(command);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(
    [FromRoute] int id,
    [FromBody] PatchProductoRequest request)
    {
        var command = new PatchProductoCommand(
            id,
            request.Codigo,
            request.Nombre,
            request.Descripcion,
            request.Precio,
            request.Activo,
            request.CategoriaId,
            request.CantidadStock
        );

        var result = await _mediator.Send(command);

        if (result is null)
            return NotFound();

        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteProductoCommand(id));

        if (!result)
            return NotFound();

        return NoContent();
    }
}
