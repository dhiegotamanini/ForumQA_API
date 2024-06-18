using ForumQA.Domain.Models;

namespace ForumQA.Domain.Abstration
{
    public interface IForumRepository
    {
        List<Forum> GetForums();
        Forum GetForum(int id);
        void AddForum(Forum forum);
        void UpdateForum(Forum forum);
        void DeleteForum(int id);
        List<Post> GetPosts();
        List<ForumType> GetForumsTypes();
        ForumType GetForumType(int forumTypeId);

        void AddForumType(ForumType forumType);
        void UpdateForumType(ForumType forumType);
        void DeleteForumType(int forumTypeId);

    }
}
