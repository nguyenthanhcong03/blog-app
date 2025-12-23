using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;

namespace BlogApp.Application.IServices;

public interface IAuthService
{
    public void Register(RegisterRequestDto register);
    public AuthResponseDto Login(LoginRequestDto login);
    public void Logout(LogoutRequestDto logout);
}