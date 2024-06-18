using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;

namespace ForumQA.Infrastructure
{
    public class UserRepository : IUserRepository
    {

        private readonly ISqlDatabaseCommands _databaseCommands;
        public UserRepository(ISqlDatabaseCommands databaseCommands)
        {
            _databaseCommands = databaseCommands;
        }
        public List<User> GetUsers()
        {
            string sql = @"SELECT  
                                  U.Id UserId,
                                  U.[Name],
                                  U.Username, 
                                  U.Photo UserPhoto, 
                                  U.MemberSince MemberSince
                           FROM   [USER] U ";

            return _databaseCommands.GetList<User>(sql);
        }

        public User GetUser(int id)
        {
            string sql = @"SELECT  
                                  U.Id UserId, 
                                  U.[Name],
                                  U.Username, 
                                  U.Photo UserPhoto, 
                                  U.MemberSince MemberSince
                                  --,U.UserPhotoBytes 
                           FROM   [USER] U 
                           WHERE U.Id = @Id ";

            return _databaseCommands.GetItem<User>(sql, id );
        }

        public void UpdateUser(User user)
        {
            string sql = @"UPDATE [User] 
                           SET    Name = @Name, 
                                  MemberSince = GETDATE(), 
                                  Username = @Username ,
                                  Photo = @PhotoFileName,
                                  PasswordHash = @UserPassword  
                           WHERE  Id = @UserId ";

            _databaseCommands.ExecuteCommand(sql, user );
        }
        public void AddUser(User user)
        {
            string sql = @"INSERT INTO [User] (Name, Photo, MemberSince,  Username,  PasswordHash ) 
                                VALUES
                                             (@Name, @PhotoFileName, GETDATE() ,  @Username, @UserPassword ) ";

            _databaseCommands.ExecuteCommand(sql, user);
        }

        public void DeleteUser(int id)
        {
            string sql = @"DELETE 
                           FROM    [User] 
                           WHERE   Id = @Id";

            _databaseCommands.ExecuteCommand(sql, new { Id = id });
        }

    }
}
