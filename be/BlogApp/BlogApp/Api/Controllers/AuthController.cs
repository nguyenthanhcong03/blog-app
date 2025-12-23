using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;
using BlogApp.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public ActionResult<ApiResponse<object>> Register([FromBody]RegisterRequestDto register) {
        _authService.Register(register);
        var response = new ApiResponse<object>
        {
            Status = 200,
            Message = "Register successful",
        };
        
        return Ok(response);
    }
    
    [HttpPost("login")]
    public ActionResult<ApiResponse<AuthResponseDto>> Register([FromBody]LoginRequestDto login) {
        
        var response = new ApiResponse<AuthResponseDto>
        {
            Status = 200,
            Message = "Register successful",
            Data = _authService.Login(login)
        };
        
        return Ok(response);
    }
    
    [HttpPost("logout")]
    [Authorize]
    public ActionResult<ApiResponse<object>> Register([FromBody]LogoutRequestDto logout) {
        _authService.Logout(logout);
        var response = new ApiResponse<object>
        {
            Status = 200,
            Message = "Register successful",
        };
        
        return Ok(response);
    }
}