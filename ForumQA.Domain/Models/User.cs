namespace ForumQA.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string? UserPassword { get; set; }
        public string? UserPhoto { get; set; }
        public string? PhotoFileName { get; set; }
        public DateTime MemberSince { get; set; }
        public byte[]? UserPhotoBytes { get; set; }
        

    }
}
