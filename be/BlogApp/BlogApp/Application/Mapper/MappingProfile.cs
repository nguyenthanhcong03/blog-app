using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Request.Authenticate;
using BlogApp.Application.DTO.Request.Category;
using BlogApp.Application.DTO.Response;
using BlogApp.Domain.Models;

namespace BlogApp.Application.Mapper;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<RegisterRequestDto, User>().ForSourceMember(src => src.ConfirmPassword, 
            opt => opt.DoNotValidate());
        CreateMap<UserProfileRequestDto, User>();
        CreateMap<User, UserProfileResponseDto>();
        
        // Category
        CreateMap<CreateCategoryRequestDto, Category>();
        CreateMap<Category, CategoryResponseDto>();
    }
}