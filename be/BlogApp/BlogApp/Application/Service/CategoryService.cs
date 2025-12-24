using AutoMapper;
using BlogApp.Application.DTO.Request.Category;
using BlogApp.Application.IRepositories;
using BlogApp.Application.IServices;
using BlogApp.Domain.Models;

namespace BlogApp.Application.Service;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }
    
    public void AddCategory(CreateCategoryRequestDto category)
    {
        _categoryRepository.AddCategory(_mapper.Map<Category>(category));
    }
}