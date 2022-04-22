namespace FTAAPI.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public int Tickets { get; set; }
        public int? DestinationId { get; set; }
        public Destination? Destination { get; set; }
    }
}
