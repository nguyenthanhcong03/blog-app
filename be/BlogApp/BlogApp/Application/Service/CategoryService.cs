using AutoMapper;
using BlogApp.Application.DTO.Request.Category;
using BlogApp.Application.DTO.Response;
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
        var categoryToAdd = _mapper.Map<Category>(category);
        categoryToAdd.Name = category.Name.Trim().ToUpper();        
        _categoryRepository.AddCategory(categoryToAdd);
    }

    public List<CategoryResponseDto> GetCategories()
    {
        return _mapper.Map<List<CategoryResponseDto>>(_categoryRepository.GetCategories());
    }


}