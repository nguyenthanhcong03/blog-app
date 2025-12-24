using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;
using BlogApp.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPatch("updateProfile")]
    public ActionResult<ApiResponse<UserProfileResponseDto>> UpdateUserProfile([FromBody] UserProfileRequestDto userProfileRequestDto)
    {
        var response = new ApiResponse<UserProfileResponseDto>
        {
            Status = 200,
            Message = "Register successful",
            Data = _userService.UpdateProfile(userProfileRequestDto)
        };
        
        return Ok(response);
    }
    
    /*[HttpPatch("updateAvatar")]
    public ActionResult<ApiResponse<string>> UpdateAvatar([FromForm] FormFile avatar)
    {
        var response = new ApiResponse<string>
        {
            Status = 200,
            Message = "Register successful",
            Data = _userService.UpdateAvatar(avatar)
        };
        
        return Ok(response);
    }*/
}