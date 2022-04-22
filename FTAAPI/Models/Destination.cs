namespace FTAAPI.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Brief { get; set; }
        public string? Content { get; set; }
        public string? Date { get; set; }
        public float Rating { get; set; }
        public List<DestinationImage>? DestinationImages { get; set; }
    }
}
