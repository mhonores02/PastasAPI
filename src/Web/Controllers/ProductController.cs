using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Exceptions;

namespace PastasAPI.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_productService.GetAll());
    }

    [HttpGet("category/{category}")]
    public IActionResult GetByCategory([FromRoute] string category)
    {
        return Ok(_productService.GetByCategory(category));
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            return Ok(_productService.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    public IActionResult Create([FromBody] ProductCreateRequest request)
    {
        var newProduct = _productService.Create(request);
        return Ok(newProduct);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    public IActionResult Update([FromRoute] int id, [FromBody] ProductUpdateRequest request)
    {
        try
        {
            _productService.Update(id, request);
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
            _productService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}