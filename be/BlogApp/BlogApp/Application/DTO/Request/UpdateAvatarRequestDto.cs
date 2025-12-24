using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Application.DTO.Request;

public class UpdateAvatarRequestDto
{
    public IFormFile AvatarFile { get; set; } = null!;
}