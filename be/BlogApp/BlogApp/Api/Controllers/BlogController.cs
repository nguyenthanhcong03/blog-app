using System.Security.Claims;
using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;
using BlogApp.Infrastructure.ExternalServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly IUploadService _uploadService;
    public BlogController(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }
    
    [HttpPost("/image/upload")]
    [Authorize]
    public ActionResult<ApiResponse<object>> UploadImage([FromForm] UploadImageRequestDto image)
    {
        var imageUrl = _uploadService.UploadImageAsync(image.Image);
        var response = new ApiResponse<object>
        {
            Status = 200,
            Message = "Register successful",
            Data = imageUrl
        };
        
        return Ok(response);
    }
}