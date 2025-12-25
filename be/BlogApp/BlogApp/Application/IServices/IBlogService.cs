using BlogApp.Application.DTO.Page;
using BlogApp.Application.DTO.Request.Blog;
using BlogApp.Application.DTO.Response;

namespace BlogApp.Application.IServices;

public interface IBlogService
{
    Task CreateAsync(CreateBlogRequestDto request, string email);
    PagedResult<BlogResponseDto> GetPageBlog(BlogQueryDto dto);
    PagedResult<BlogResponseDto> GetMyBlog(string email, BlogQueryDto dto);
    Task PublishBlog(int id);
}