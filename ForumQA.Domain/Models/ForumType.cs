namespace ForumQA.Domain.Models
{
    public class ForumType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserCreated { get; set; }
    }
}
