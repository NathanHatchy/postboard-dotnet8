using PostBoard.Models;

namespace PostBoard.Services;

public interface IPostService
{
    Task<IReadOnlyList<Post>> GetPostsAsync(CancellationToken cancellationToken = default);
    Task<Post?> GetPostAsync(int id, CancellationToken cancellationToken = default);
    Task<Post> CreatePostAsync(Post post, CancellationToken cancellationToken = default);
}
