using System.ComponentModel.DataAnnotations;

namespace PostBoard.Models;

public sealed class Post
{
    public int Id { get; init; }

    [Display(Name = "User ID")]
    [Range(1, 10, ErrorMessage = "User ID must be between 1 and 10.")]
    public int UserId { get; set; } = 1;

    [Required]
    [StringLength(120, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000, MinimumLength = 10)]
    public string Body { get; set; } = string.Empty;
}
