namespace BlogApp.Application.DTO.Request;

public record UserProfileRequestDto
(
    string? FirstName,
    string? LastName,
    string? UserName,
    string? Email
    );