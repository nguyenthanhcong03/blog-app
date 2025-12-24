using System.Security.Claims;
using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Request.Authenticate;
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
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<ApiResponse<string>>> UploadImage([FromForm] UploadImageRequestDto image)
    {
        var imageUrl = await _uploadService.UploadImageAsync(image.Image);
        var response = new ApiResponse<object>
        {
            Status = 200,
            Message = "Upload image successful",
            Data = imageUrl
        };
        
        return Ok(response);
    }
}