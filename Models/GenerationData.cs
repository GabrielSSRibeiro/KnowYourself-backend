namespace server.Models
{
    public class GenerationData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
    }
}