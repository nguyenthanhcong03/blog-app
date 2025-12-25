using BlogApp.Application.DTO.Request.Blog;

namespace BlogApp.Application.IServices;

public interface IBlogService
{
    Task CreateAsync(CreateBlogRequestDto request, string email);
}