using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;

namespace ForumQA.Infrastructure
{
    public class AnswerPostRepository : IAnswerPostRepository
    {
        private readonly ISqlDatabaseCommands _databaseCommands;
        public AnswerPostRepository(ISqlDatabaseCommands databaseCommands)
        {
            _databaseCommands = databaseCommands;
        }
        public List<AnswerPost> GetAnswerPost(int postId)
        {
            
            string sql = @" SELECT      
                                       A.Id, 
                                       A.Answer, 
                                       A.AnswerDate, 
                                       A.UserId, 
                                       A.PostId,  
                                       P.TITLE, 
                                       P.Description, 
                                       A.UserId
                            FROM       AnswerPost A
                            INNER JOIN POST P ON A.PostId = P.Id
                            WHERE      A.PostId = @postId";
            return _databaseCommands.GetList<AnswerPost>(sql, postId, true);
        }

        public bool AddAnswerPost(AnswerPost answerPost)
        {
            string sql = @" INSERT INTO AnswerPost (Answer, AnswerDate, UserId, PostId )  
                            VALUES                 (@answer, Getdate(), @userId, @postId ) ";
            _databaseCommands.ExecuteCommand(sql, answerPost);

            return true;
        }
    }
}
