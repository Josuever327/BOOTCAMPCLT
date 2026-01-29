using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Command.CreateProduct;
using Api.BootCamp.Aplication.Command.DeleteProducto;
using Api.BootCamp.Aplication.Command.PatchProducto;
using Api.BootCamp.Aplication.Command.UpdateProduct;
using Api.BootCamp.Aplication.Query.GetProductById;
using Api.BootCamp.Aplication.Query.GetProductos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.BootCamp.Api.Controllers;

/// <summary>
/// Controlador para la gestión de productos.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene la lista de productos, opcionalmente filtrados por categoría.
    /// </summary>
    /// <param name="categoriaId">
    /// Identificador de la categoría para filtrar los productos. Es opcional.
    /// </param>
    /// <returns>
    /// Devuelve una colección de productos.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetAll(
        [FromQuery] int? categoriaId)
    {
        if (categoriaId is <= 0)
            return BadRequest("El categoriaId debe ser mayor a cero.");

        var productos = await _mediator.Send(new GetProductosQuery(categoriaId));

        return Ok(productos);
    }


    /// <summary>
    /// Obtiene un producto por su identificador.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <returns>Producto encontrado.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductoResponse>> GetById([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser mayor a cero.");

        var producto = await _mediator.Send(new GetProductoByIdQuery(id));

        if (producto is null)
            return NotFound();

        return Ok(producto);
    }

    /// <summary>
    /// Crea un nuevo producto.
    /// </summary>
    /// <param name="command">Datos del producto a crear.</param>
    /// <returns>Producto creado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductoResponse>> Create(
        [FromBody] CreateProductoCommand command)
    {
        var producto = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = producto.Id },
            producto);
    }

    /// <summary>
    /// Actualiza completamente un producto existente.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <param name="command">Datos actualizados del producto.</param>
    /// <returns>Producto actualizado.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductoResponse>> Update(
        [FromRoute] int id,
        [FromBody] UpdateProductoCommand command)
    {
        if (id <= 0 || id != command.Id)
            return BadRequest("El id de la ruta no coincide con el cuerpo.");

        var producto = await _mediator.Send(command);

        if (producto is null)
            return NotFound();

        return Ok(producto);
    }

    /// <summary>
    /// Actualiza parcialmente un producto existente.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <param name="command">Datos parciales del producto.</param>
    /// <returns>Producto actualizado.</returns>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductoResponse>> Patch(
        [FromRoute] int id,
        [FromBody] PatchProductoCommand command)
    {
        if (id <= 0 || id != command.Id)
            return BadRequest("El id de la ruta no coincide con el cuerpo.");

        var producto = await _mediator.Send(command);

        if (producto is null)
            return NotFound();

        return Ok(producto);
    }

    /// <summary>
    /// Elimina un producto por su identificador.
    /// </summary>
    /// <param name="id">Identificador del producto.</param>
    /// <returns>Resultado de la eliminación.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
            return BadRequest("El id debe ser mayor a cero.");

        var eliminado = await _mediator.Send(new DeleteProductoCommand(id));

        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}

