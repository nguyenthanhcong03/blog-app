using BlogApp.Domain.Models;

namespace BlogApp.Application.IRepositories;

public interface IUserRepository
{
    void Register(User register);
    public User GetUserByEmail(string email);
}   