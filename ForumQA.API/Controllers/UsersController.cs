using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ForumQA.API.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IWebHostEnvironment _env;

        public UsersController(IUserService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Login(Login login)
        {
            return Ok(true);
        }


        [HttpGet]
        [Route("users")]
        public ActionResult<PageResults<User>> GetUsers(int? pageIdex, int? itemsPerPage)
        {
            var rs = _service.GetUsers(pageIdex ?? 1 , itemsPerPage ?? 25);
            return new JsonResult(new { rs?.Items, rs?.TotalItems, rs?.PageIndex, rs?.TotalPages, rs?.ErrorMessage, rs?.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpGet]
        [Route("users/{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var rs = _service.GetUser(id);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpPut("users/{id}")]
        public ActionResult<User> UpdateUser(User user, int id)
        {
            var rs = _service.UpdateUser(user, id);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpPost("users")]
        public ActionResult<User> AddUser(User user)
        {
            var rs = _service.AddUser(user);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpDelete("users/{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            var rs = _service.DeleteUser(id);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }


        [HttpPost("users-update")]
        public IActionResult SaveUser([FromForm] string user, [FromForm] IFormFile? photo)
        {
            var userObj = JsonConvert.DeserializeObject<User>(user);

            if (photo != null && photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyToAsync(memoryStream);
                    userObj.UserPhotoBytes = memoryStream.ToArray();
                }

                userObj.PhotoFileName = photo.FileName;
                userObj.PhotoFileName = SaveFileInFolder(photo);
            }

            var rs  = _service.UpdateUser(userObj, userObj.UserId);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }


        [HttpPost("add-users")]
        public IActionResult NewUser([FromForm] string user, [FromForm] IFormFile? photo)
        {
            var userObj = JsonConvert.DeserializeObject<User>(user);

            if (photo != null && photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyToAsync(memoryStream);
                    userObj.UserPhotoBytes = memoryStream.ToArray();
                }

                userObj.PhotoFileName = photo.FileName;
                userObj.PhotoFileName = SaveFileInFolder(photo);
            }

            var rs = _service.AddUser(userObj);
            return new JsonResult(new { rs.Data, rs.Message, rs.StatusCode }) { StatusCode = rs.StatusCode };
        }

        [HttpGet("users/{photoName}/picture")]
        public IActionResult GetImage(string photoName)
        {
            var directory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf(@"bin\")));
            _env.WebRootPath = directory;
            var filePath = Path.Combine(_env.WebRootPath, "images");
            filePath += "\\" + photoName;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, "image/jpeg");

        }

        private string SaveFileInFolder(IFormFile file)
        {
            try
            {
                string fileSaved = @"images\";
                var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf(@"bin\")));
                _env.WebRootPath = path;
                var imagePath = Path.Combine(_env.WebRootPath, "images");

                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                string filePath = Path.Combine(imagePath, file.FileName);
                fileSaved += file.FileName;

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return fileSaved;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

    }
}
