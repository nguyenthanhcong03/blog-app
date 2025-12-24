using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;

namespace BlogApp.Application.IServices;

public interface IUserService
{
    UserProfileResponseDto UpdateProfile(UserProfileRequestDto userProfileRequestDto);
   // Task<string?> UpdateAvatarAsync(IFormFile file);
}