using BlogApp.Domain.Models;
namespace BlogApp.Application.DTO.Request;

public record LogoutRequestDto
(
    string RefreshToken
);