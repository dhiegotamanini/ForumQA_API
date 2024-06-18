namespace ForumQA.Domain.Models
{
    public class ForumQAResult<T>
    {
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
