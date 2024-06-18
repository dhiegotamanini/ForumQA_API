namespace ForumQA.Domain.Models
{
    public class Forum
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Post>? Posts { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserCreated { get; set; }
        public int ForumTypeId { get; set; }
    }
}
