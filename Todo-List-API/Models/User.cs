namespace Todo_List_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
        public ICollection<Category> Categories { get; set; }
    }
}
