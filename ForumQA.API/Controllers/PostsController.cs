using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumQA.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("posts/{forumId}")]
        public ActionResult<PageResults<Post>> GetPosts(int forumId, int? pageIndex , int? itemsPerPage)
        {
            var rs = _postService.GetPosts(forumId , pageIndex ?? 1, itemsPerPage ?? 25);
            return new JsonResult(new { rs?.Items, rs?.TotalItems, rs?.PageIndex, rs?.TotalPages, rs?.ErrorMessage, rs?.StatusCode }) { StatusCode = rs.StatusCode };
        }


        [HttpGet("answer-post/{postId}")]
        public ActionResult<PageResults<AnswerPost>> AnswerPost(int postId, int? pageIndex, int? itemsPerPage)
        {
            var rs = _postService.GetPosts(postId, pageIndex ?? 1, itemsPerPage ?? 25);
            return new JsonResult(new { rs?.Items, rs?.TotalItems, rs?.PageIndex, rs?.TotalPages, rs?.ErrorMessage, rs?.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpPost("add-post")]
        public ActionResult<bool> AddPost([FromBody] Post post)
        {
            _postService.AddPost(post);
            return new JsonResult(true);
        }

    }
}
