namespace ForumQA.Domain.Models
{
    public class PageResults<T>
    {
        public List<T>? Items { get; set; }
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public string? ErrorMessage { get; set; }
        public int? StatusCode { get; set; }
    }
}
