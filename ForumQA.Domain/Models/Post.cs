namespace ForumQA.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostDate { get; set; }
        public int ForumId { get; set; }
        public int UserId { get; set; }

    }

}
