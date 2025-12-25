using BlogApp.Domain.Models;

namespace BlogApp.Application.IRepositories;

public interface IBlogRepository
{
    Task AddAsync(Blog blog);
}