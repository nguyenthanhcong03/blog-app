using AutoMapper;
using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;
using BlogApp.Application.IRepositories;
using BlogApp.Application.IServices;
using BlogApp.Application.MiddleWare;
using BlogApp.Domain.Models;
using BlogApp.Infrastructure.ExternalServices;

namespace BlogApp.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _map;
    private readonly UploadService _uploadService;
    public UserService(IUserRepository userRepository, IMapper map,  UploadService uploadService)
    {
        _uploadService = uploadService;
        _map = map;
        _userRepository = userRepository;
    }
    
    public UserProfileResponseDto UpdateProfile(UserProfileRequestDto userProfileRequestDto)
    {
        var user = _map.Map<User>(userProfileRequestDto);

        _userRepository.UpdateProfile(user);
            
        return _map.Map<UserProfileResponseDto>(user);
    }

    /*public async Task<string?> UpdateAvatarAsync(IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            throw new AppException(ErrorCode.FileIsEmpty);
        }
        
        var avatarUrl = _uploadService.
    }*/
}