namespace Todo_List_API.Models
{
    public class ListCategory
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public List List { get; set; }
        public int ListId { get; set; }
        public Category? Category { get; set; } = null;
        public int? CategoryId { get; set; } = null;
    }
}
