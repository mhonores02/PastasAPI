using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Exceptions;

namespace PastasAPI.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "RequireAdminRole")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_adminService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(_adminService.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] AdminCreateRequest request)
    {
        var newAdmin = _adminService.Create(request);
        return Ok(newAdmin);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] AdminUpdateRequest request)
    {
        try
        {
            _adminService.Update(id, request);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            _adminService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}