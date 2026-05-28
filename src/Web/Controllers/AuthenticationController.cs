using Microsoft.AspNetCore.Mvc;
using PastasAPI.Application.Interfaces;
using PastasAPI.Application.Models.Requests;
using PastasAPI.Domain.Exceptions;

namespace PastasAPI.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] CredentialsRequest credentials)
    {
        try
        {
            string token = _authenticationService.Authenticate(credentials);
            return Ok(token);
        }
        catch (NotAllowedException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}