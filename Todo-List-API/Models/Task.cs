namespace Todo_List_API.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } = null;
        public DateTime? Deadline { get; set; } = null;
        public User? AssignedTo { get; set; } = null;
        public int? AssignedToId { get; set; } = null;
        public List List { get; set; }
        public int ListId { get; set; }
        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public DateTime? CompletedAt { get; set; } = null;
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
