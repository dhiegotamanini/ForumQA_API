using ForumQA.Domain.Models;

namespace ForumQA.Domain.Abstrations
{
    public interface IPostService
    {
        List<Post> GetPosts(int forumId);
        PageResults<Post> GetPosts(int forumId, int pageIndex, int itemsPerPage);
        ForumQAResult<Post> DeletePost(int id);
        ForumQAResult<Post> AddPost(Post post);
        ForumQAResult<Post> UpdatePost(Post post, int id);
    }
}
