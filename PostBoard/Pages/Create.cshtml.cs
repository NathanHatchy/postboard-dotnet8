using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostBoard.Models;
using PostBoard.Services;

namespace PostBoard.Pages;

public sealed class CreateModel : PageModel
{
    private readonly IPostService _postService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(IPostService postService, ILogger<CreateModel> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    [BindProperty]
    public Post Post { get; set; } = new();

    public string? ErrorMessage { get; private set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Mimics post creation by redirecting to Created page along with post data

        try
        {
            var createdPost = await _postService.CreatePostAsync(Post, cancellationToken);
            TempData["CreatedPostId"] = createdPost.Id;
            TempData["CreatedPostTitle"] = createdPost.Title;
            return RedirectToPage("/Created");
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            _logger.LogWarning(exception, "Unable to create a post through JSONPlaceholder.");
            ErrorMessage = "The post could not be submitted right now. Please try again.";
            return Page();
        }
    }
}
