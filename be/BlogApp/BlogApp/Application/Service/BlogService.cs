using BlogApp.Application.DTO.Request.Blog;
using BlogApp.Application.IRepositories;
using BlogApp.Application.IServices;
using BlogApp.Application.MiddleWare;
using BlogApp.Domain.Models;
using BlogApp.Infrastructure.Repositories;
using Slugify;

namespace BlogApp.Application.Service;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUserRepository _userRepository;

    public BlogService(IBlogRepository blogRepository,  ICategoryRepository categoryRepository,  
        ITagRepository tagRepository, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _tagRepository = tagRepository;
        _categoryRepository = categoryRepository;
        _blogRepository = blogRepository;
    }
    
    bool ExistCategoryById(int categoryId)
    {
        return (_categoryRepository.GetCategoryById(categoryId) != null);
    }
    
    public async Task CreateAsync(CreateBlogRequestDto request, string email)
    {
        User user = _userRepository.GetUserByEmail(email);
        if (user == null) throw new AppException(ErrorCode.UserNotFound);
        
        var blog = new Blog
        {
            Title = request.Title,
            Content = request.Content,
            Slug = request.Slug,
            PublishedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            AuthorId = user.Id
        };

        if (request.Categories?.Any() == true )
        {
            foreach (var categoryId in request.Categories)
            {
                if (!ExistCategoryById(categoryId)) throw new AppException(ErrorCode.CategoryNotExist);
            }
            
            blog.BlogCategories = request.Categories
                .Distinct()
                .Select(categoryId => new BlogCategory
                {
                    CategoryId = categoryId
                })
                .ToList();
        }
        
        // xu ly tag
        if (request.TagNames?.Any() == true)
        {
            var normalizedTagNames = request.TagNames
                .Select(t => t.Trim().ToLower())
                .Distinct()
                .ToList();
            
            // tag da ton tai
            var existingTags  = _tagRepository.GetExistingTask(normalizedTagNames);
            
            // tag chua co
            var existingTagNames = existingTags
                .Select(t => t.Name.ToLower())
                .ToHashSet();
            
            // thu vien slug
            var slugHelper = new SlugHelper();

            var newTags = normalizedTagNames
                .Where(name => !existingTagNames.Contains(name))
                .Select(name => new Tag
                {
                    Name = name,
                    Slug = slugHelper.GenerateSlug(name),
                })
                .ToList();
            
            if (newTags.Any())
            {
                _tagRepository.AddMultipleTag(newTags);
                existingTags.AddRange(newTags);
            }
            
            blog.BlogTags = existingTags
                .Select(tag => new BlogTag
                {
                    TagId = tag.Id
                })
                .ToList();
        }
        
        await _blogRepository.AddAsync(blog);
    }
}