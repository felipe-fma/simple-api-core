using System.Net;
using simple_api_core.DTO.Request;
using simple_api_core.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using simple_api_core.Services.Interface;

namespace simple_api_core.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    IBooksService _service;

    public BooksController(IBooksService service) => _service = service;

    [Route("")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookResponse>), (int)HttpStatusCode.OK)]
    public IActionResult Get() => Ok(_service.GetAll().ToArray());

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(BookResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public IActionResult GetBookDetails([FromRoute] int id)
    {
        try
        {
            return Ok(_service.GetById(id));
        }
        catch (System.Exception)
        {
            return NotFound("Nada foi localizado esse ID");
        }
    }


    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<BookResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public IActionResult Post([FromBody] BooksQueryParameters filter)
    {
        try
        {
            return Ok(_service.GetByParameters(filter));
        }
        catch (System.Exception)
        {
            return NotFound("Nada foi localizado com esses parametros");
        }
    }

    [Route("{id}/shipping")]
    [HttpGet]
    [ProducesResponseType(typeof(ShippingResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public IActionResult GetBookShipping([FromRoute] int id) 
    {
        try
        {
            return Ok(_service.ShippingPrice(id));
        }
        catch (System.Exception)
        {
            return NotFound("Nada foi localizado esse ID");
        }
    }
}
