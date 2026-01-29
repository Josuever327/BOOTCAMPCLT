using Api.BootCamp.Api.Response;
using Api.BootCamp.Aplication.Command.CreateProduct;
using Api.BootCamp.Aplication.Command.CreateProducto;
using Api.BootCamp.Aplication.Command.DeleteProducto;
using Api.BootCamp.Aplication.Command.PatchProducto;
using Api.BootCamp.Aplication.Command.UpdateProducto;
using Api.BootCamp.Aplication.Query.GetProductById;
using Api.BootCamp.Aplication.Query.GetProductoById;
using Api.BootCamp.Aplication.Query.GetProductos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.BootCamp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetAll([FromQuery] int? categoriaId)
        {
            if (categoriaId is <= 0)
                return BadRequest("El categoriaId debe ser mayor a cero.");

            var productos = await _mediator.Send(new GetProductosQuery(categoriaId));
            return Ok(productos);
        }

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
                return NotFound($"Producto con id {id} no encontrado");

            return Ok(producto);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoResponse>> Create([FromBody] CreateProductoCommand command)
        {
            var producto = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = producto.Id },
                producto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoResponse>> Update([FromRoute] int id, [FromBody] UpdateProductoCommand command)
        {
            if (id != command.Id)
                return BadRequest("El id de la ruta no coincide con el id del cuerpo");

            var producto = await _mediator.Send(command);

            if (producto is null)
                return NotFound($"Producto con id {id} no encontrado");

            return Ok(producto);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoResponse>> Patch([FromRoute] int id, [FromBody] PatchProductoCommand command)
        {
            if (id != command.Id)
                return BadRequest("El id de la ruta no coincide con el id del cuerpo");

            var producto = await _mediator.Send(command);

            if (producto is null)
                return NotFound($"Producto con id {id} no encontrado");

            return Ok(producto);
        }

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
                return NotFound($"Producto con id {id} no encontrado");

            return NoContent();
        }
    }
}
