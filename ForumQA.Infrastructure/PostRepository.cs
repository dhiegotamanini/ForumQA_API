using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;

namespace ForumQA.Infrastructure
{
    public class PostRepository : IPostRepository
    {

        private readonly ISqlDatabaseCommands _databaseCommands;
        public PostRepository(ISqlDatabaseCommands databaseCommands)
        {
            _databaseCommands = databaseCommands;
        }
        public PageResults<Post> GetPosts(int pageIndex, int itemsPerPage)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPosts(int forumId)
        {
            string sql = @"SELECT 
                                  Id, 
                                  Title, 
                                  Description, 
                                  PostDate, 
                                  ForumType AS ForumId 
                           FROM   Post 
                           WHERE  ForumType = @forumType ";
            return _databaseCommands.GetList<Post>(sql, forumId);
        }

        public Post GetPost(int id)
        {
            throw new NotImplementedException();
        }

        public void AddPost(Post post)
        {
            string sql = "INSERT INTO POST (Title,  Description,  PostDate, ForumType) " +
                         "VALUES           (@Title, @Description, GetDate(), @ForumId )  ";

            _databaseCommands.ExecuteCommand(sql, post);
        }

        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(int id)
        {
            throw new NotImplementedException();
        }
    }
}
