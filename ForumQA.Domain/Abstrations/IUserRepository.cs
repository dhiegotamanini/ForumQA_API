using ForumQA.Domain.Models;

namespace ForumQA.Domain.Abstrations
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(int id);

        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
