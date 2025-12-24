using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;
using BlogApp.Application.IServices;
using Microsoft.AspNetCore.Authorization;
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
   // [Authorize]
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
    
    /*[HttpPatch("avatar")]
    public async Task<ActionResult<ApiResponse<string>>> UpdateAvatar([FromForm] IFormFile avatar)
    {
        var result = await _userService.UpdateAvatarAsync(avatar);

        var response = new ApiResponse<string>
        {
            Status = 200,
            Message = "Update avatar successful",
            Data = result
        };

        return Ok(response);
    }*/
}