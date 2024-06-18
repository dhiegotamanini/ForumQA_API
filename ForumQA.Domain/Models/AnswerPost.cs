namespace ForumQA.Domain.Models
{
    public class AnswerPost
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public DateTime AnswerDate { get; set; }
        public int UserId { get; set; }

        public User? User { get; set; }

        public int PostId { get; set; }
    }
}
