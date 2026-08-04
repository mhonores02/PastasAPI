using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Exceptions;

namespace PastasAPI.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "RequireClientRole")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public IActionResult GetMyCart()
    {
        try
        {
            return Ok(_cartService.GetByClientId(GetUserId()));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("products")]
    public IActionResult AddProduct([FromBody] AddProductToCartRequest request)
    {
        try
        {
            return Ok(_cartService.AddProduct(GetUserId(), request));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (NotAllowedException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("products/{productId}")]
    public IActionResult RemoveProduct([FromRoute] int productId)
    {
        try
        {
            return Ok(_cartService.RemoveProduct(GetUserId(), productId));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("checkout")]
    public IActionResult Checkout()
    {
        try
        {
            return Ok(_cartService.Checkout(GetUserId()));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (NotAllowedException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}