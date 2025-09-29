namespace API.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<UserPost>? UserPosts { get; set; }
    }
}
