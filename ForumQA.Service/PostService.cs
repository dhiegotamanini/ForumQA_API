using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;
using ForumQA.Domain.Utils;

namespace ForumQA.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }
        public List<Post> GetPosts(int forumId)
        {
            return _repository.GetPosts(forumId);
        }

        public PageResults<Post> GetPosts(int forumId, int pageIndex, int itemsPerPage)
        {
            var rs = _repository.GetPosts(forumId);
            return FormatPageResult(pageIndex, itemsPerPage, rs);
        }

        public ForumQAResult<Post> UpdatePost(Post post, int id)
        {
            try
            {
                var rs = _repository.GetPost(id);

                if (id == 0 || rs == null)
                {
                    return Result.GetReturnFormated<Post>(post, "Post not found", 404);
                }

                _repository.UpdatePost(post);

                return Result.GetReturnFormated<Post>(post, "Post has been updated", 204);
            }
            catch (Exception ex)
            {
                return Result.GetReturnFormated<Post>(null, ex.Message, 500);
            }
            
        }

        public ForumQAResult<Post> AddPost(Post post)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(post.Title))
                {
                    return Result.GetReturnFormated<Post>(post, "Post must be title", 400);
                }

                if (string.IsNullOrWhiteSpace(post.Description))
                {
                    return Result.GetReturnFormated<Post>(post, "Post must be description", 400);
                }

                _repository.AddPost(post);
                return Result.GetReturnFormated<Post>(post, "Post has been created", 204);
            }
            catch (Exception ex)
            {
                return Result.GetReturnFormated<Post>(null, ex.Message, 500);
            }
            
        }

        public ForumQAResult<Post> DeletePost(int id)
        {
            try
            {
                var post = _repository.GetPost(id);
                if (post == null)
                {
                    return Result.GetReturnFormated<Post>(post, "Post not found", 404);
                }

                _repository.DeletePost(id);

                return Result.GetReturnFormated<Post>(post, "Post has been removed", 204);
            }
            catch (Exception ex)
            {
                return Result.GetReturnFormated<Post>(null, ex.Message, 500);
            }

        }

        private PageResults<Post> FormatPageResult(int pageIndex, int itemsPerPage, List<Post> rs)
        {
            return new PageResults<Post>()
            {
                TotalItems = rs?.Count ?? 0,
                Items = rs?.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                PageIndex = pageIndex,
                TotalPages = rs?.Count > 0 ? (int)Math.Ceiling(rs.Count / (double)itemsPerPage) : 0,
                StatusCode = 200
            };
        }
    }
}
