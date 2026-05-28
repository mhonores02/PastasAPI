using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Exceptions;

namespace PastasAPI.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] ClientCreateRequest request)
    {
        var newClient = _clientService.Create(request);
        return Ok(newClient);
    }

    [HttpGet]
    [Authorize(Policy = "RequireAdminRole")]
    public IActionResult GetAll()
    {
        return Ok(_clientService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "RequireClientRole")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(_clientService.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "RequireClientRole")]
    public IActionResult Update([FromRoute] int id, [FromBody] ClientUpdateRequest request)
    {
        try
        {
            _clientService.Update(id, request);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            _clientService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}