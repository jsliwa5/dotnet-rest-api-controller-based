namespace dotnet_rest_api_controller_based.Models
{
    public class User
    {
        int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
