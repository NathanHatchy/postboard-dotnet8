using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostBoard.Models;
using PostBoard.Services;

namespace PostBoard.Pages;

public sealed class DetailsModel : PageModel
{
    private readonly IPostService _postService;

    public DetailsModel(IPostService postService)
    {
        _postService = postService;
    }

    public Post Post { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        // Shows post data

        var post = await _postService.GetPostAsync(id, cancellationToken);
        if (post is null)
        {
            return NotFound();
        }

        Post = post;
        return Page();
    }
}
