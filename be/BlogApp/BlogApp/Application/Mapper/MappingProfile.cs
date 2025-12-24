using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;
using BlogApp.Domain.Models;

namespace BlogApp.Application.Mapper;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {

        CreateMap<RegisterRequestDto, User>().ForSourceMember(src => src.ConfirmPassword, 
            opt => opt.DoNotValidate());

        CreateMap<UserProfileRequestDto, User>();
        CreateMap<User, UserProfileResponseDto>();
    }
}