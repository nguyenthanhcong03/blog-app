using BlogApp.Application.MiddleWare;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BlogApp.Infrastructure.ExternalServices;

public class UploadService
{
    private readonly Cloudinary _cloudinary;
    private static readonly string[] AllowedExtensions =
    {
        ".jpg", ".jpeg", ".png", ".webp"
    };

    private static readonly string[] AllowedContentTypes =
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };
    
    public UploadService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }
    
    public static void ValidateImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new AppException(ErrorCode.ImageNotAllowed);

        // Size (ví dụ: 5MB)
        if (file.Length > 5 * 1024 * 1024)
            throw new AppException(ErrorCode.ImageTooLarge);

        // Extension
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
            throw new AppException(ErrorCode.ImageNotAllowed);

        // Content-Type
        if (!AllowedContentTypes.Contains(file.ContentType))
            throw new AppException(ErrorCode.ContentTypeImageNotAllowed);
    }


    public async Task<string?> UploadImageAsync(IFormFile file)
    {
        if (file.Length == 0)
            return null;

        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "products",
            Transformation = new Transformation()
                .Width(800)
                .Height(800)
                .Crop("limit")
                .Quality("auto")
                .FetchFormat("auto")
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error != null)
            throw new Exception(result.Error.Message);

        return result.SecureUrl.ToString();
    }
}