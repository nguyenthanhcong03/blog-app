namespace BlogApp.Application.DTO.Request;

public record VerifyOtpRequestDto(
    string Email,
    string Otp
    );