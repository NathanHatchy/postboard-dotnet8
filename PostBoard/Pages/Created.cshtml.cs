using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PostBoard.Pages;

public sealed class CreatedModel : PageModel
{
    public int PostId { get; private set; }
    public string PostTitle { get; private set; } = string.Empty;

    public IActionResult OnGet()
    {
        if (TempData["CreatedPostId"] is not int postId)
        {
            return RedirectToPage("/Index");
        }

        // Shows post title as created from prior page

        PostId = postId;
        PostTitle = TempData["CreatedPostTitle"] as string ?? "Your post";
        return Page();
    }
}
