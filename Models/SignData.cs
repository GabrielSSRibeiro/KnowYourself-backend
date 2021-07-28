namespace server.Models
{
    public class SignData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FDetails { get; set; }
        public string MDetails { get; set; }
        public int MinDate { get; set; }
        public int MaxDate { get; set; }
    }
}