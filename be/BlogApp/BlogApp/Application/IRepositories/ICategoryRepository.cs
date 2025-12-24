using BlogApp.Domain.Models;

namespace BlogApp.Application.IRepositories;

public interface ICategoryRepository
{
    void AddCategory(Category category);
}