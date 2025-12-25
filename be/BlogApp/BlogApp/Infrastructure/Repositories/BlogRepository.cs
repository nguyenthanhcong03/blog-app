using System.Runtime.InteropServices.JavaScript;
using BlogApp.Application.IRepositories;
using BlogApp.Application.MiddleWare;
using BlogApp.Domain.Enums;
using BlogApp.Domain.Models;
using BlogApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public IQueryable<Blog> GetPageBlog()
    {
        return _db.Blogs
            .Include(b => b.Author)
            .Include(b => b.BlogCategories)
                .ThenInclude(bc => bc.Category)
            .Include(b => b.BlogTags)
                .ThenInclude(bt => bt.Tag)
            .AsQueryable();
    }

    public async Task PublishBlog(int id)
    {
        var blog =  _db.Blogs.FirstOrDefault(b => b.Id == id);

        if (blog == null) throw new AppException(ErrorCode.BlogIsNotExist);
        
        blog.PublishedAt = DateTime.UtcNow;
        blog.Status = BlogStatus.Published;
         _db.Blogs.Update(blog);
        await _db.SaveChangesAsync();
    }

}