using ForumQA.Domain.Abstration;
using ForumQA.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumQA.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ForumsController : ControllerBase
    {
        private readonly IForumService _forumService;
        public ForumsController(IForumService forumService)
        {
            _forumService = forumService;
        }

        [HttpGet("forums")]
        public ActionResult<PageResults<Forum>> GetForums(int? pageIndex, int? itemsPerPage)
        {
            var rs = _forumService.GetForums(pageIndex ?? 1 , (itemsPerPage == null || itemsPerPage.Value == 0) ? 25 : itemsPerPage.Value);
            return new JsonResult(new { rs?.Items, rs?.TotalItems, rs?.PageIndex , rs?.TotalPages, rs?.ErrorMessage, rs?.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpGet("forums/{id}")]
        public ActionResult<Forum> GetForum(int id)
        {
            var rs = _forumService.GetForum(id);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };

        }

        [HttpPut("forums/{id}")]
        public ActionResult<Forum> UpdateForum(Forum forum , int id )
        {
            _forumService.UpdateForum(forum , id);
            return Ok(true);
        }

        [HttpPost("forums")]
        public ActionResult<Forum> AddForum(Forum forum)
        {
            _forumService.AddForum(forum);
            return Ok(true);
        }

        [HttpDelete("forums/{id}")]
        public ActionResult<Forum> DeleteForum(int id)
        {
            _forumService.DeleteForum(id);
            return Ok(true);
        }

        [HttpGet("forum-types")]
        public ActionResult<PageResults<ForumType>> GetForumTypes(int? pageIndex, int? itemsPerPage)
        {
            var rs = _forumService.GetForumsTypes(pageIndex ?? 1, (itemsPerPage == null || itemsPerPage.Value == 0) ? 25 : itemsPerPage.Value);
            return new JsonResult(new { rs?.Items, rs?.TotalItems, rs?.PageIndex, rs?.TotalPages, rs?.ErrorMessage, rs?.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpGet("forum-types/{forumTypeId}")]
        public ActionResult<ForumType> GetForumType(int forumTypeId)
        {
            var rs = _forumService.GetForumType(forumTypeId);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpPut("forum-types/{forumTypeId}")]
        public ActionResult<ForumType> UpdateForumType(ForumType forumType, int forumTypeId)
        {
            var rs = _forumService.UpdateForumType(forumType, forumTypeId);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpPost("forum-types")]
        public ActionResult<ForumType> AddForumType(ForumType forumType)
        {
            var rs =_forumService.AddForumType(forumType);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpDelete("forum-types/{forumTypeId}")]
        public ActionResult<ForumType> DeleteForumType(int forumTypeId)
        {
            var rs = _forumService.DeleteForumType(forumTypeId);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }


    }
}
