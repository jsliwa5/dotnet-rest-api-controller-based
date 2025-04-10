namespace dotnet_rest_api_controller_based.Models
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Email {  get; set; }

        public string? Descritption { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
