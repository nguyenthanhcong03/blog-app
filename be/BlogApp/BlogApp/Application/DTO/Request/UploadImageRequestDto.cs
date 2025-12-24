namespace BlogApp.Application.DTO.Request;

public class UploadImageRequestDto
{
    public IFormFile Image { get; set; } = null!;
}