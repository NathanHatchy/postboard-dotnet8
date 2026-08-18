using System.Net;
using System.Net.Http.Json;
using PostBoard.Models;

namespace PostBoard.Services;

public sealed class JsonPlaceholderPostService : IPostService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<JsonPlaceholderPostService> _logger;

    public JsonPlaceholderPostService(HttpClient httpClient, ILogger<JsonPlaceholderPostService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Post>> GetPostsAsync(CancellationToken cancellationToken = default)
    {
        var posts = await _httpClient.GetFromJsonAsync<List<Post>>("posts", cancellationToken);
        return posts ?? new List<Post>();
    }

    public async Task<Post?> GetPostAsync(int id, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync($"posts/{id}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Post>(cancellationToken: cancellationToken);
    }

    public async Task<Post> CreatePostAsync(Post post, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync("posts", post, cancellationToken);
        response.EnsureSuccessStatusCode();

        var createdPost = await response.Content.ReadFromJsonAsync<Post>(cancellationToken: cancellationToken);
        if (createdPost is null)
        {
            _logger.LogError("JSONPlaceholder returned an empty response when creating a post.");
            throw new InvalidOperationException("The post service returned an empty response.");
        }

        return createdPost;
    }
}
