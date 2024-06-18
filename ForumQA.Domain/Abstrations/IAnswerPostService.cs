using ForumQA.Domain.Models;

namespace ForumQA.Domain.Abstrations
{
    public interface IAnswerPostService
    {
        PageResults<AnswerPost> GetAnswerPost(int postId, int pageIndex, int itemsPerPage);
        bool AddAnswerPost(AnswerPost answerPost);
    }
}
