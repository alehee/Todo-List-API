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
        public ICollection<List> CreatedLists { get; set; }
        public ICollection<ListCategory> AssignedLists { get; set; }
        public ICollection<Task> AssignedTasks { get; set; }
        public ICollection<Task> CreatedTasks { get; set; }
        
    }
}
