using ForumQA.Domain.Models;

namespace ForumQA.Domain.Abstrations
{
    public interface IPostRepository
    {
        List<Post> GetPosts(int forumId);
        Post GetPost(int id);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);        
    }
}
