namespace Todo_List_API.Models.Common
{
    public class Response
    {
        public string Type { get; set; } = "SUCCESS";
        public object? Message { get; set; } = null;
    }
}
