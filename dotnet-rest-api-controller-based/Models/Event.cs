namespace dotnet_rest_api_controller_based.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }

        public ICollection<User> Attendees { get; set; } = new List<User>();
        public Organizer? Organizer { get; set; }
        public int OrganizerId { get; set; }
    }
}
