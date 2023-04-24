namespace Todo_List_API.Models
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public int OwnerId { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<ListCategory> AssignedUsers { get; set; }
    }
}
