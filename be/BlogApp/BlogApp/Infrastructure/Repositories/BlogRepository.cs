using BlogApp.Application.IRepositories;
using BlogApp.Domain.Models;
using BlogApp.Infrastructure.Persistence;

namespace BlogApp.Infrastructure.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly AppDbContext _db;

    public BlogRepository(AppDbContext dbContext)
    {
        _db = dbContext;
    }
    
    public async Task AddAsync(Blog blog)
    {
        await _db.Blogs.AddAsync(blog);
        await _db.SaveChangesAsync();
    }
}