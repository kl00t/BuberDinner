using BuberDinner.Application.Services;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("api/[controller]")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authenticationResult = _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authenticationResult.Match(
            authenticationResult => Ok(MapAuthenticationResult(authenticationResult)),
            errors => Problem(errors)
            );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authenticationResult = _authenticationService.Login(
            request.Email,
            request.Password);

        if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials )
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized);
        }

        return authenticationResult.Match(
            authenticationResult => Ok(MapAuthenticationResult(authenticationResult)),
            errors => Problem(errors)
            );
    }

    private static AuthenticationResponse MapAuthenticationResult(AuthenticationResult authenticationResult)
    {
        return new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token);
    }
}