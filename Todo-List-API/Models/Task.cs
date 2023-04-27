namespace Todo_List_API.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public User? AssignedTo { get; set; }
        public int? AssignedToId { get; set; }
        public List List { get; set; }
        public int ListId { get; set; }
        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
