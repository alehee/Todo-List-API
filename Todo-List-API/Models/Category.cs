namespace Todo_List_API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public ICollection<List> Lists { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; } = null;
    }
}
