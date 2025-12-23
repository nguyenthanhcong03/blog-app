using AutoMapper;
using BlogApp.Application.DTO.Request;
using BlogApp.Application.DTO.Response;
using BlogApp.Application.IRepositories;
using BlogApp.Application.IServices;
using BlogApp.Application.MiddleWare;
using BlogApp.Domain.Enums;
using BlogApp.Domain.Models;
using BlogApp.Infrastructure.ExternalServices;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Application.Service;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    public readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _map;
    private readonly TokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
    
    public AuthService(IUserRepository userRepository,  IMapper map,  TokenService tokenService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _map = map;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public void Register(RegisterRequestDto register)
    {
        if (register.ConfirmPassword != register.Password)
        {
            throw new AppException(ErrorCode.ConfirmPasswordIsNotMatch);
        }
        User newUser = _map.Map<User>(register);

        newUser.Role = UserRole.User;
        newUser.Password = _passwordHasher.HashPassword(newUser, register.Password);
        
        _userRepository.Register(newUser);
    }

    public AuthResponseDto Login(LoginRequestDto login)
    {
        User user = _userRepository.GetUserByEmail(login.Email);
        if (user is null)
        {
            throw new AppException(ErrorCode.UserNotFound); 
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);
        
        if (result != PasswordVerificationResult.Success)
        {
            throw new AppException(ErrorCode.PasswordIsNotMatch);
        }
        
        var token = _tokenService.GenerateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user);
                
        return new AuthResponseDto(token, refreshToken.Token);
    }

    public void Logout(LogoutRequestDto logout)
    {
        var token = _refreshTokenRepository.GetRefreshToken(logout.RefreshToken);
        if (token is null)
        {
            throw new AppException(ErrorCode.NotAuthenticate);
        }
        
        _refreshTokenRepository.DeleteRefreshToken(token);
    }
}