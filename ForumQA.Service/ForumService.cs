using ForumQA.Domain.Abstration;
using ForumQA.Domain.Models;

namespace ForumQA.Service
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;
        public ForumService(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        public PageResults<Forum> GetForums(int pageIndex, int itemsPerPage)
        {
            var rs = _forumRepository.GetForums();
            var posts = _forumRepository.GetPosts();
            rs.ForEach(x => x.Posts = posts.Where(c => c.ForumId == x.Id).ToList());
            return FormatPageResult(pageIndex, itemsPerPage, rs);
        }

        public ForumQAResult<Forum> GetForum(int id)
        {
            try
            {
                var rs = _forumRepository.GetForum(id);
                if (rs == null)
                {
                    return GetReturnFormated<Forum>(rs, "Not Found", 404);
                }

                return GetReturnFormated<Forum>(rs, "Sucess return data", 200);
            }
            catch (Exception ex)
            {
                return GetReturnFormated<Forum>(null, ex.Message, 500);
            }
        }

        public void UpdateForum(Forum forum, int id)
        {
            _forumRepository.UpdateForum(forum);
        }
        public void AddForum(Forum forum)
        {
            _forumRepository.AddForum(forum);
        }
        public void DeleteForum(int id)
        {
            _forumRepository.DeleteForum(id);
        }

        private PageResults<T> FormatPageResult<T>(int pageIndex, int itemsPerPage, List<T> rs)
        {
            return new PageResults<T>()
            {
                TotalItems = rs?.Count ?? 0,
                Items = rs?.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                PageIndex = pageIndex,
                TotalPages = rs?.Count > 0 ? (int)Math.Ceiling(rs.Count / (double)itemsPerPage) : 0,
                StatusCode = 200
            };
        }


        public PageResults<ForumType> GetForumsTypes(int pageIndex, int itemsPerPage)
        {
            var rs = _forumRepository.GetForumsTypes();
            return FormatPageResult<ForumType>(pageIndex, itemsPerPage, rs);
        }

        public ForumQAResult<ForumType> GetForumType(int forumTypeId)
        {
            try
            {
                var rs = _forumRepository.GetForumType(forumTypeId);
                if (rs == null)
                {
                    return GetReturnFormated<ForumType>(rs, "Not Found", 404);
                }

                return GetReturnFormated<ForumType>(rs, "Sucess return data", 200);
            }
            catch (Exception ex)
            {
                return GetReturnFormated<ForumType>(null, ex.Message, 500);
            }

        }

        public ForumQAResult<ForumType> DeleteForumType(int forumTypeId)
        {
            try
            {
                var forumType = _forumRepository.GetForumType(forumTypeId);
                if (forumType == null)
                {
                    return GetReturnFormated<ForumType>(forumType, "Forum type not found", 404);
                }
                
                _forumRepository.DeleteForumType(forumTypeId);

                return GetReturnFormated<ForumType>(forumType, "Forum type has been removed", 204);
            }
            catch (Exception ex)
            {
                return GetReturnFormated<ForumType>(null, ex.Message, 500);
            }

        }
        public ForumQAResult<ForumType> AddForumType(ForumType forumType)
        {
            try
            {
                var forumsTypes = _forumRepository.GetForumsTypes();
                if (forumsTypes.Count > 0)
                {
                    if (forumsTypes.Any(x => x.Name.Trim().ToLower() == forumType.Name.Trim().ToLower()))
                    {
                        return GetReturnFormated<ForumType>(forumType, "Already exist forum type with this name", 400);
                    }

                    if (forumsTypes.Any(x => x.UserCreated == 0))
                    {
                        return GetReturnFormated<ForumType>(forumType, "Please, inform an userCreated for create new forum type", 400);
                    }
                }

                _forumRepository.AddForumType(forumType);
                return GetReturnFormated<ForumType>(forumType, "Forum type has been created successful", 200);

            }
            catch (Exception ex)
            {
                return GetReturnFormated<ForumType>(null, ex.Message, 500);
            }
            
        }

        public ForumQAResult<ForumType> UpdateForumType(ForumType forumType, int forumTypeId)
        {
            try
            {
                var forumTypeDb = _forumRepository.GetForumType( forumTypeId);
                if (forumTypeDb == null)
                {
                    return GetReturnFormated<ForumType>(forumType, "Forum type not found", 400);
                }

                if (string.IsNullOrWhiteSpace(forumTypeDb.Name))
                {
                    return GetReturnFormated<ForumType>(forumType, "Please, inform a name for update forum type", 400);
                }

                if (forumTypeDb.UserCreated == 0)
                {
                    return GetReturnFormated<ForumType>(forumType, "Please, inform an UserCreated for update forum type", 400);
                }

                _forumRepository.AddForumType(forumType);
                return GetReturnFormated<ForumType>(forumType, "Forum type has been updated successfully", 204);

            }
            catch (Exception ex)
            {
                return GetReturnFormated<ForumType>(null, ex.Message, 500);
            }
        }

        private static ForumQAResult<T> GetReturnFormated<T>(T rs, string message, int statusCode)
        {
            return new ForumQAResult<T>()
            {
                Data = rs,
                Message = message,
                StatusCode = statusCode
            };
        }

    }
}
