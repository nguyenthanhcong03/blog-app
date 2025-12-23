using BlogApp.Domain.Models;

namespace BlogApp.Application.IRepositories;

public interface IUserRepository
{
    void Register(User register);
    public void AddRefreshToken(RefreshToken token);
    public void DeleteRefreshToken(RefreshToken token);
    public RefreshToken GetRefreshToken(string refreshToken);
    public User GetUserByEmail(string email);
}   