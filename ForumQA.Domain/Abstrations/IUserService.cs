using ForumQA.Domain.Models;

namespace ForumQA.Domain.Abstrations
{
    public interface IUserService
    {
        PageResults<User> GetUsers(int pageIndex, int itemsPerPage);
        ForumQAResult<User> GetUser(int id);
        ForumQAResult<User> DeleteUser(int id);
        ForumQAResult<User> AddUser(User user);
        ForumQAResult<User> UpdateUser(User user, int id);
    }
}
