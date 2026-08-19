using Microsoft.AspNetCore.Mvc.RazorPages;
using PostBoard.Models;
using PostBoard.Services;

namespace PostBoard.Pages;

public sealed class IndexModel : PageModel
{
    private readonly IPostService _postService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IPostService postService, ILogger<IndexModel> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    public IReadOnlyList<Post> Posts { get; private set; } = Array.Empty<Post>();
    public string? ErrorMessage { get; private set; }

    // Gets post data for use in for loop to present posts
    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            Posts = await _postService.GetPostsAsync(cancellationToken);
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            _logger.LogWarning(exception, "Unable to load posts from JSONPlaceholder.");
            ErrorMessage = "Posts could not be loaded right now. Please try again.";
        }
    }
}
