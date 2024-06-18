using ForumQA.Domain.Models;

namespace ForumQA.Domain.Abstration
{
    public interface IForumService
    {
        PageResults<Forum> GetForums(int pageIndex, int itemsPerPage);
        ForumQAResult<Forum> GetForum(int id);
        void DeleteForum(int id);
        void AddForum(Forum forum);
        void UpdateForum(Forum forum, int id);

        
        PageResults<ForumType> GetForumsTypes(int pageIndex, int itemsPerPage);
        ForumQAResult<ForumType> GetForumType(int forumTypeId);

        ForumQAResult<ForumType> DeleteForumType(int forumTypeId);
        ForumQAResult<ForumType> AddForumType(ForumType forumType);
        ForumQAResult<ForumType> UpdateForumType(ForumType forumType, int forumTypeId);
    }
}
