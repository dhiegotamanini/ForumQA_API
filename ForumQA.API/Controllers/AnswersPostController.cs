using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumQA.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AnswersPostController : ControllerBase
    {
        private readonly IAnswerPostService _answerPostService;
        public AnswersPostController(IAnswerPostService answerPostService)
        {
            _answerPostService = answerPostService;
        }

        [HttpGet("answers-post/{postId}")]
        public ActionResult<AnswerPost> GetAnswersPost(int postId, int? pageIndex, int? itemsPerPage)
        {
            var rs = _answerPostService.GetAnswerPost(postId, pageIndex ?? 1, itemsPerPage ?? 25);
            return new JsonResult(new { rs?.Items, rs?.TotalItems, rs?.PageIndex, rs?.TotalPages, rs?.ErrorMessage, rs?.StatusCode }) { StatusCode = rs?.StatusCode };
        }

        [HttpPost("answers-post")]
        public ActionResult<bool> AddAnswerPost([FromBody] AnswerPost answerPost)
        {
            return _answerPostService.AddAnswerPost(answerPost);
        }

    }
}
