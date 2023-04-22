namespace Todo_List_API.Models
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category? Category { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
