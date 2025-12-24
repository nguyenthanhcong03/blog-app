using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.DTO.Request.Blog;

public record CreateBlogRequestDto
(
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    string Title,

    [Required(ErrorMessage = "Slug is required")]
    [MaxLength(200, ErrorMessage = "Slug cannot exceed 100 characters")]
    string Slug,

    [Required(ErrorMessage = "Content is required")]
    string Content,

    // Category và Tag có thể để trống lúc tạo Draft
    List<int> CategoryIds,
    List<int> TagIds
);
