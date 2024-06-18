using ForumQA.Domain.Models;

namespace ForumQA.Domain.Abstrations
{
    public interface IAnswerPostRepository
    {
        List<AnswerPost> GetAnswerPost(int postId);
        bool AddAnswerPost(AnswerPost answerPost);
    }
}
