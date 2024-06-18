using ForumQA.Domain.Abstration;
using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;

namespace ForumQA.Infrastructure
{
    public class ForumRepository : IForumRepository
    {
        private readonly ISqlDatabaseCommands _databaseCommands;
        public ForumRepository(ISqlDatabaseCommands databaseCommands)
        {
            _databaseCommands = databaseCommands;
        }
        public Forum GetForum(int id)
        {
            string sql = @"SELECT Id, 
                                  [Name], 
                                  Description, 
                                  DateCreated, 
                                  UserCreated, 
                                  ForumTypeid 
                           FROM   Forum 
                           WHERE  Id = @Id ";
                    
            return _databaseCommands.GetItem<Forum>(sql, id);
        }

        public List<Forum> GetForums()
        {
            string sql = @"SELECT Id, 
                                  [Name], 
                                  Description, 
                                  DateCreated, 
                                  UserCreated, 
                                  ForumTypeid 
                           FROM   Forum ";
            return _databaseCommands.GetList<Forum>(sql);
        }

        public List<Post> GetPosts()
        {
            string sql = @"SELECT Id, 
                                  Title, 
                                  Description, 
                                  PostDate, 
                                  ForumType AS ForumId  
                           FROM   Post ";
            return _databaseCommands.GetList<Post>(sql);
        }

        public void UpdateForum(Forum forum)
        {
            string sql = @"UPDATE Forum 
                           SET    Name = @Name, 
                                  Description = @Description, 
                                  DateCreated = GETDATE(), 
                                  UserCreated = @UserCreated, 
                                  ForumTypeId = @ForumTypeId 
                           WHERE  Id = @Id ";
            _databaseCommands.ExecuteCommand(sql, forum);
        }
        public void AddForum(Forum forum)
        {
            string sql = @"INSERT INTO Forum (Name, Description, DateCreated, UserCreated, ForumTypeId ) 
                                VALUES
                                             (@Name, @Description, GETDATE(), @UserCreated, @ForumTypeId ) ";
            _databaseCommands.ExecuteCommand(sql, forum);
        }

        public void DeleteForum(int id)
        {
            string sql = @"DELETE 
                           FROM    Forum 
                           WHERE   Id = @Id";

            _databaseCommands.ExecuteCommand(sql, new { Id = id });
        }

        public List<AnswerPost> GetAnswer(int id)
        {
            string sql = @" SELECT 
                                       A.Id, 
                                       A.Answer, 
                                       A.AnswerDate, 
                                       A.UserId, 
                                       A.PostId, 
                                       P.Title, 
                                       P.Description, 
                                       P.ForumType 
                            FROM       AnswerPost A 
                            INNER JOIN Post P ON A.PostId = P.Id ";

            return _databaseCommands.GetList<AnswerPost>(sql, id);
        }

        public ForumType GetForumType(int forumTypeId)
        {
            string sql = @"SELECT Id,
                                  Name,
                                  DateCreated,
                                  UserCreated 
                           FROM   ForumType 
                           WHERE  Id = @Id ";

            return _databaseCommands.GetItem<ForumType>(sql, forumTypeId);
        }

        public List<ForumType> GetForumsTypes()
        {
            string sql = @"SELECT Id,
                                  Name,
                                  DateCreated,
                                  UserCreated 
                           FROM   ForumType   ";
            return _databaseCommands.GetList<ForumType>(sql);
        }

        public void AddForumType(ForumType forumType)
        {
            string sql = @"INSERT INTO ForumType (Name, DateCreated, UserCreated ) 
                                VALUES
                                                 (@Name, GETDATE(), @UserCreated ) ";

            _databaseCommands.ExecuteCommand(sql, forumType);
        }

        public void UpdateForumType(ForumType forumType)
        {
            string sql = @"UPDATE ForumType 
                            SET   Name  = @Name, 
                                  DateCreated = GETDATE(), 
                                  UserCreated= @UserCreated 
                            WHERE Id = @Id ";

            _databaseCommands.ExecuteCommand(sql, forumType);
        }

        public void DeleteForumType(int forumTypeId)
        {
            string sql = @"DELETE 
                           FROM    ForumType 
                           WHERE   Id = @Id";

            _databaseCommands.ExecuteCommand(sql, new { id = forumTypeId });
        }
    }
}
