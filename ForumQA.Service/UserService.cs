using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;
using ForumQA.Domain.Utils;

namespace ForumQA.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public PageResults<User> GetUsers(int pageIndex, int itemsPerPage) 
        {
            var rs = _userRepository.GetUsers();
            return Result.FormatPageResult<User>(pageIndex, itemsPerPage, rs);
        }

        public ForumQAResult<User> GetUser(int id)
        {
            try
            {
                var rs = _userRepository.GetUser(id);
                if (rs == null)
                {
                    return Result.GetReturnFormated<User>(rs, "Not Found", 404);
                }

                return Result.GetReturnFormated<User>(rs, "Sucess return data", 200);
            }
            catch (Exception ex)
            {
                return Result.GetReturnFormated<User>(null, ex.Message, 500);
            }
        }

        public ForumQAResult<User> DeleteUser(int id)
        {
            try
            {
                var userDb = _userRepository.GetUser(id);
                if (userDb == null)
                {
                    return Result.GetReturnFormated<User>(userDb, "User not found", 404);
                }

                _userRepository.DeleteUser(id);

                return Result.GetReturnFormated<User>(userDb, "User has been removed", 204);
            }
            catch (Exception ex)
            {
                return Result.GetReturnFormated<User>(null, ex.Message, 500);
            }

        }
        public ForumQAResult<User> AddUser(User user)
        {
            try
            {
                var userDb = _userRepository.GetUsers();
                if (userDb.Count > 0)
                {
                    if (userDb.Any(x => x.Name.Trim().ToLower() == user.Name.Trim().ToLower()))
                    {
                        return Result.GetReturnFormated<User>(user, "Already exist user with this name", 400);
                    }

                    if (userDb.Any(x => x.UserId == 0))
                    {
                        return Result.GetReturnFormated<User>(user, "Please, inform an userCreated for create new user", 400);
                    }
                }

                _userRepository.AddUser(user);

                return Result.GetReturnFormated<User>(user, "User has been created successful", 200);

            }
            catch (Exception ex)
            {
                return Result.GetReturnFormated<User>(null, ex.Message, 500);
            }


        }
        public ForumQAResult<User> UpdateUser(User user, int id)
        {
            try
            {
                var userDb = _userRepository.GetUser(id);
                if (userDb == null)
                {
                    return Result.GetReturnFormated<User>(userDb, "User not found", 400);
                }

                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    return Result.GetReturnFormated<User>(user, "Please, inform a name for update user", 400);
                }

                //if (userDb.UserId == 0)
                //{
                //    return Result.GetReturnFormated<User>(userDb, "Please, inform an UserCreated for update forum type", 400);
                //}

                //if(user.UserPhotoBytes != null)
                //{
                //    user.UserPhoto = Convert.ToBase64String(user.UserPhotoBytes);
                //}


                _userRepository.UpdateUser(user);
                return Result.GetReturnFormated<User>(user, "User has been updated successfully", 204);

            }
            catch (Exception ex)
            {
                return Result.GetReturnFormated<User>(null, ex.Message, 500);
            }

        }

    }
}
