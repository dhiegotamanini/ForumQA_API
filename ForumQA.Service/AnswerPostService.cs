using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;

namespace ForumQA.Service
{
    public class AnswerPostService : IAnswerPostService
    {
        private readonly IAnswerPostRepository _answerPostRepository;
        private readonly IUserService _userService;

        public AnswerPostService(IAnswerPostRepository answerPostRepository, IUserService userService)
        {
            _answerPostRepository = answerPostRepository;
            _userService = userService;
        }

        public PageResults<AnswerPost> GetAnswerPost(int postId, int pageIndex, int itemsPerPage)
        {
            var rs = _answerPostRepository.GetAnswerPost(postId);
            var users = _userService.GetUsers(pageIndex, itemsPerPage);
            var result = rs.Select(x => new AnswerPost
            {
                Id = x.Id,
                Answer = x.Answer,
                AnswerDate = x.AnswerDate,
                UserId = x.UserId,
                User = users?.Items?.FirstOrDefault(u => u.UserId == x.UserId),
                PostId = x.PostId,                

            }).ToList();

            var userTreat = users?.Items
                            .Where(x => x.UserPhoto != "" && x.UserPhoto != null)
                            .Select(x => new User
                            {
                                PhotoFileName = 
                                x.UserPhoto.Substring(  users.Items. FirstOrDefault(u => u.UserId == x.UserId).UserPhoto.LastIndexOf(@"\") + 1,
                                   users.Items?.FirstOrDefault(u => u.UserId == x.UserId).UserPhoto.Length > 0 ?
                                   (users.Items.FirstOrDefault(u => u.UserId == x.UserId).UserPhoto.Length ) -
                                   (users.Items.FirstOrDefault(u => u.UserId == x.UserId).UserPhoto.LastIndexOf(@"\") + 1 ) 
                                        : 0
                                        )
                                ,
                                UserId = x.UserId   ,
                                MemberSince = x.MemberSince
                            }).ToList();


            foreach (var item in result)
            {
                foreach (var user in userTreat)
                {
                    if(user.UserId == item.UserId)
                    {
                        item.User.PhotoFileName = user.PhotoFileName;
                    }
                }
            }
            
            return FormatPageResult(pageIndex, itemsPerPage, result);
        }

        public bool AddAnswerPost(AnswerPost answerPost)
        {
            return _answerPostRepository.AddAnswerPost(answerPost);
        }

        private PageResults<AnswerPost> FormatPageResult(int pageIndex, int itemsPerPage, List<AnswerPost> rs)
        {
            return new PageResults<AnswerPost>()
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
