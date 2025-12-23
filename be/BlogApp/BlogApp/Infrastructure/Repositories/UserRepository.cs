using AutoMapper;
using BlogApp.Application.IRepositories;
using BlogApp.Domain.Models;
using BlogApp.Infrastructure.Persistence;

namespace BlogApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    public UserRepository(AppDbContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }
    
    public void AddRefreshToken(RefreshToken token)
    {
        _db.RefreshTokens.Add(token);
        _db.SaveChanges();
    }

    public RefreshToken GetRefreshToken(string token)
    {
        return _db.RefreshTokens.FirstOrDefault(e => e.Token == token);
    }

    public void DeleteRefreshToken(RefreshToken token)
    {
        _db.RefreshTokens.Remove(token);
        _db.SaveChanges();
    }

    public void Register(User user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public User GetUserByEmail(string email)
    {
        return _db.Users.FirstOrDefault(e => e.Email == email);
    }
}